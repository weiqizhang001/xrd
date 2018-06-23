# -*- coding: utf-8 -*-
"""
Description: Curve Fitting with Pearson VII
Literature: Support vector machine with a Pearson VII function kernel for discriminating halophilic and non-halophilic proteins (Part 2.3)
Author: fyt
FirstDate: 2018_03_30
LastDate: 2018_05_04
Add: 
    1. 功能函数化
    2. 添加注释
    3. 添加最小二乘拟合直线
    4. 添加直线和曲线拟合可视化
    5. 添加应力测试
"""

import numpy as np  
import matplotlib.pyplot as plt  
from scipy import optimize  
import sys

# ---------
# Parameter
disApproX = 10  # 该参数表示无线接近水平最标轴的距离
n = 1.5
# ---------------------------------------
# F_PearsonVII: Function Pearson VII 定义 
# Input:
#      x: Pearson VII函数自变量  
#      p_H: peak higth  
#      p_x0: center of peak 
#      p_omega: control tailing factor of peak
#      p_sigma: control half-width of peak
# Output:
#       y: x: Pearson VII函数因变量  
def F_PearsonVII(x, p_H, p_x0, p_omega, p_sigma):  
    y = p_H / ((1 + ((2 * (x - p_x0) * np.sqrt(2 ** (1 / p_omega) - 1)) / p_sigma) ** 2) ** p_omega)
    return y

def F_PearsonVII_15(x, peak_height, x_center, sigma):  
    y = peak_height / ((1 + ((2 * (x - x_center) * np.sqrt(2 ** (1 / n) - 1)) / sigma) ** 2) ** n)
    return y

def F_PearsonVII_Jade(x, peak_height_jade, x_center_Jade, FWHM_Jade): 
    y = peak_height_jade / np.power((1 + ((4 * (2 ** (1 / n) - 1)) / (FWHM_Jade ** 2)) * np.power((x - x_center_Jade), 2)), n)
    return y
	
# -------------------------------------------------
# F_PearsonVII_T: Reverse Function Pearson VII 定义
# Input:
#      y: PearsonVII逆函数自变量  
#      p_H: peak higth  
#      p_x0: center of peak 
#      p_omega: control tailing factor of peak
#      p_sigma: control half-width of peak
# Output:
#       x: PearsonVII逆函数因变量  
def F_PearsonVII_T(y, p_H, p_x0, p_omega, p_sigma):  
    x = ((p_sigma * np.sqrt(pow(p_H / y, (1 / p_omega)) - 1)) / (2 * np.sqrt(2 ** (1 / p_omega) - 1))) + p_x0
    return x 

# ----------------------------------
# F_CurveFit: Pearson VII函数拟合曲线
# Input:    
#      number: 拟合点的个数
#      x: 拟合自变量
#      y: 拟合因变量
#      pattern: 
#             0: 不扣除背景
#             1: 0.85法扣除背景
#             2: 曲线水平拟合扣除背景
# Output:
#       xCenter: 波峰中心 
#       H: 波峰中心高度
#       D: 半峰宽
#       yShow: 拟合后的y值
def F_CurveFit(number, x, y, pattern):
    if pattern == 0:
        H, xCenter, omega, sigma = optimize.curve_fit(F_PearsonVII, x, y)[0]  
        yShow = H / ((1 + ((2 * (x - xCenter) * np.sqrt(2 ** (1/omega) - 1)) / sigma) ** 2) ** omega) 
        xHalfH = F_PearsonVII_T(H/2, H, xCenter, omega, sigma)
        D = abs(xHalfH - xCenter) * 2
        return xCenter, H, D, yShow
    
    if pattern == 1:
        yMin = min(y) * 0.85
        yNew = y - np.ones(number) * yMin
        H, xCenter, omega, sigma = optimize.curve_fit(F_PearsonVII, x, yNew)[0]  
        yShow = H / ((1 + ((2 * (x - xCenter) * np.sqrt(2 ** (1/omega) - 1)) / sigma) ** 2) ** omega) 
        xHalfH = F_PearsonVII_T(H/2, H, xCenter, omega, sigma)
        D = abs(xHalfH - xCenter) * 2
        return xCenter, H, D, yShow
    
    if pattern == 2:
        H, xCenter, omega, sigma = optimize.curve_fit(F_PearsonVII, x, y)[0] 
        xMinY = F_PearsonVII_T(disApproX, H, xCenter, omega, sigma)  
        distHalf = abs(xCenter - xMinY)  
        xMin = xCenter - distHalf
        xMax = xCenter + distHalf
        xShow = np.arange(xMin, xMax, 0.1)
        yShow = F_PearsonVII(xShow, H, xCenter, omega, sigma)
        xHalfH = F_PearsonVII_T(H/2, H, xCenter, omega, sigma)
        D = abs(xHalfH - xCenter) * 2
        return xCenter, H, D, yShow

    if pattern == 3:
        H, xCenter, sigma = optimize.curve_fit(F_PearsonVII_15, x, y)[0]  
        omega = 1.5
        yShow = H / ((1 + ((2 * (x - xCenter) * np.sqrt(2 ** (1/omega) - 1)) / sigma) ** 2) ** omega) 
        xHalfH = F_PearsonVII_T(H/2, H, xCenter, omega, sigma)
        D = abs(xHalfH - xCenter) * 2
        return xCenter, H, D, yShow
    
    if pattern == 4:
        H, xCenter, D = optimize.curve_fit(F_PearsonVII_Jade, x, y)[0]  
        omega = 1.5
        yShow = H / np.power((1 + ((4 * (2 ** (1 / n) - 1)) / (D ** 2)) * np.power((x - xCenter), 2)), n) 
        #xHalfH = F_PearsonVII_T(H/2, H, xCenter, omega, sigma)
        #D = abs(xHalfH - xCenter) * 2
        return xCenter, H, D, yShow	
# ------------------------------------
# F_LeastSquare: 最小二乘法拟合直线参数
# Input:
#      x: 原始数据x
#      y: 原始数据y
# Output:
#       k: 一次函数y = kx + b的斜率
#       b: 一次函数y = kx + b的截距
def F_LeastSquare(x, y):
    xMean = sum(x) / len(x)   
    yMean = sum(y) / len(y)   
    xSum = 0.0
    ySum = 0.0
    for i in range(len(x)):
        xSum += (x[i] - xMean) * (y[i] - yMean)
        ySum += (x[i] - xMean)**2
    k = xSum / ySum
    b = yMean - k * xMean

    return k, b       


# -----------------------------------
# F_ShowCruveFitting: 曲线拟合结果显示
# Input:
#      x: 原始数据x
#      y: 原始数据y
#      H: Pearson VII方程参数  
#      x0: Pearson VII方程参数 
#      omega: Pearson VII方程参数 
#      sigma: Pearson VII方程参数
def F_ShowFittingPearsonVII(x, y, H, x0, omega, sigma):
    # Show_Pearson VII
    plt.figure()
    """
    # 设置背景颜色
    ax = plt.axes(axisbg='whitesmoke') 
    ax.set_axisbelow(True)     
    # 添加背景线并设置颜色
    plt.grid(color='w', linestyle='solid')  
    # 隐藏坐标系的外围框线 
    for spine in ax.spines.values(): 
        spine.set_visible(False) 
    # 隐藏上方与右侧的坐标轴刻度 
    ax.xaxis.tick_bottom() 
    ax.yaxis.tick_left() 
    # 让刻度线与标签的颜色更亮一些 
    ax.tick_params(colors='gray', direction='out') 
    for tick in ax.get_xticklabels(): 
        tick.set_color('gray') 
    for tick in ax.get_yticklabels(): 
        tick.set_color('gray') 
    """
    # 标题
    plt.title('Curve Fitting with Pearson VII')
    # 画线
    plt.scatter(x, y, color='navy', alpha=0.5, s=10)
    plt.plot(x, y, color='dimgray', alpha=0.5)
    plt.plot(x, F_PearsonVII(x, H, x0, omega, sigma), color='teal')
    plt.show()

# -------------------------------------------
# F_ShowLeastFitting: 最小二乘拟合直线结果显示
# Input:
#      x: 原始数据x
#      y: 原始数据y
def F_ShowFittingLeastSquare(x, y):
    # Show_LeastSquare
    plt.figure()
    # 设置背景颜色
    ax = plt.axes(axisbg='whitesmoke')  
    ax.set_axisbelow(True)     
    # 添加背景线并设置颜色
    plt.grid(color='w', linestyle='solid')  
    # 隐藏坐标系的外围框线 
    for spine in ax.spines.values(): 
        spine.set_visible(False) 
    # 隐藏上方与右侧的坐标轴刻度 
    ax.xaxis.tick_bottom() 
    ax.yaxis.tick_left() 
    # 让刻度线与标签的颜色更亮一些 
    ax.tick_params(colors='gray', direction='out') 
    for tick in ax.get_xticklabels(): 
        tick.set_color('gray') 
    for tick in ax.get_yticklabels(): 
        tick.set_color('gray') 
    # 标题
    plt.title('Linear Fitting with Least Square')
    # 画图
    plt.scatter(x, y, color='navy', alpha=0.5, s=10)
    k, b = F_LeastSquare(x, y)
    plt.plot(x, k * x + b, color='teal')
    plt.show()
    

# ============================================================================  Main
    
# ----------------------------------------------------------------------------  1. Pearson VII曲线拟合测试
#pathDataCruve = np.loadtxt('D:/Dat/2018_03_23_CFP/Dat/D_2018_03_26/0.txt') 
#pathDataCruve = np.loadtxt('D:/work/xrd/05.SWTools/python/gang-0.txt')


pathDataCruve = np.loadtxt(sys.argv[2])

if sys.argv[1] == '1':
    xP = pathDataCruve[:, 0]
    yP = pathDataCruve[:, 1]
    number = np.size(xP) 
    x0, H, D, yShow = F_CurveFit(number, xP, yP, int(sys.argv[3])) 
    H, xCenter, omega, sigma = optimize.curve_fit(F_PearsonVII, xP, yP)[0] 
    #F_ShowFittingPearsonVII(xP, yP, H, x0, omega, sigma)
    #omega = 1.5 fixed
    #H, xCenter, sigma = optimize.curve_fit(F_PearsonVII_15, xP, yP)[0]
    #omega = 1.5

    '''jade
    H, x0, D = optimize.curve_fit(F_PearsonVII_Jade, xP, yP)[0]
    omega = 1.5
    sigma = 0.0
    '''
    print(x0, H, D, omega, sigma, sep=',', end="\r\n")
    

elif sys.argv[1] == '2':	
# ----------------------------------------------------------------------------  2. 最小二乘法测试
    xOrg = np.array([np.sin(np.pi/12) ** 2, np.sin(np.pi/6) ** 2, np.sin(np.pi/4) ** 2])
    yOrg = np.array([-0.019, -0.069, -0.128])
    k, b = F_LeastSquare(xOrg, yOrg)
    #F_ShowFittingLeastSquare(xOrg, yOrg)
    print(k, b, sep=',', end="\r\n")
'''    
# ----------------------------------------------------------------------------  3. 应力值计算测试(原方法)
# (参数对应文档中公式的对应参数)
X = 2.2907
n = 4
E = 216
V = 0.28
d = np.ones(n)
strFile = np.array(['0.txt', '15.txt', '30.txt', '45.txt'])
# ----------------------------------------------------------------------------  [D]
for i in np.arange(4):
    datCruve = np.loadtxt('D:/Dat/2018_03_23_CFP/Dat/D_2018_03_26/' + strFile[i])
    xP = datCruve[:, 0]
    yP = datCruve[:, 1]
    number = np.size(xP) 
    x0, H, D, yShow = F_CurveFit(number, xP, yP, 0) 
    Theta = x0 / 2
    if i == 0:
        theta0 = Theta
    d[i] = X / (2 * np.sin(Theta * np.pi / 180))
# ----------------------------------------------------------------------------  [M]
xLS = np.array([0, np.sin(np.pi/12) ** 2, np.sin(np.pi/6) ** 2, np.sin(np.pi/4) ** 2])
yLS = np.array([0, (d[1]-d[0])/d[0], (d[2]-d[0])/d[0], (d[3]-d[0])/d[0]])
k, b = F_LeastSquare(xLS, yLS)
# F_ShowFittingLeastSquare(xLS, yLS)
M = k
### ----------------------------------------------------------------------------  [K]
K = -E / (2 * (1 + V)) * np.tan(((90 - theta0) * np.pi / 180))
### ----------------------------------------------------------------------------  [Stress]
Stress_A = K * M * 10000

# ----------------------------------------------------------------------------  3. 应力值计算测试 (新方法)
Stress_B = M * E / (1 + V) *1000
'''