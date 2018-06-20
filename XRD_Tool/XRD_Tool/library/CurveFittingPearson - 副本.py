# -*- coding: utf-8 -*-
"""
Description: 通过Pearson VII函数拟合曲线
Date: 2018_03_30
Author: fyt
"""

import numpy as np  
# import matplotlib.pyplot as plt  
from scipy import optimize  

#np.seterr(all='raise')

# Parameter
approX = 10  # 该参数表示无线接近水平最标轴的距离


# F_PearsonVII: Pearson VII函数表达式
def F_PearsonVII(x, p_H, p_x0, p_omega, p_sigma):  
    return p_H / ((1 + ((2 * (x - p_x0) * np.sqrt(2 ** (1 / p_omega) - 1)) / p_sigma) ** 2) ** p_omega)
    

# F_PearsonVII_T: Pearson VII逆函数表达式
def F_PearsonVII_T(y, p_H, p_x0, p_omega, p_sigma):  
    return ((p_sigma * np.sqrt(pow(p_H / y, (1 / p_omega)) - 1)) / (2 * np.sqrt(2 ** (1 / p_omega) - 1))) + p_x0


# F_CurveFit: Pearson VII函数拟合曲线
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
        xMinY = F_PearsonVII_T(approX, H, xCenter, omega, sigma)  
        distHalf = abs(xCenter - xMinY)  
        xMin = xCenter - distHalf
        xMax = xCenter + distHalf
        xShow = np.arange(xMin, xMax, 0.1)
        yShow = F_PearsonVII(xShow, H, xCenter, omega, sigma)
        xHalfH = F_PearsonVII_T(H/2, H, xCenter, omega, sigma)
        D = abs(xHalfH - xCenter) * 2
        return xCenter, H, D, yShow
    
# ----------------------------------------------
# Test
#data = np.loadtxt('D:/Script_Python/Data/0.txt')  
data = np.loadtxt('C:/work/python/0.txt')  

x = data[:, 0]
y = data[:, 1]
number = np.size(x) 
x0, H, D, yShow = F_CurveFit(number, x, y, 2) 






   

   


   