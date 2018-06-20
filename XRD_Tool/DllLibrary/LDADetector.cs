using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DllLibrary
{
    public class LDADetector
    {
        /// <summary>
        /// -get version: Returns the software version of the socket server, which is of the form 
        /// "M3.0.0".  The returned  string is terminated  by a Null character. 
        /// </summary>
        /// <returns></returns>
        public byte[] GetVersion()
        {
            string Str = "-get version";

            byte[] Cmd = null;

            try
            {
                Cmd = System.Text.Encoding.ASCII.GetBytes(Str);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
            }

            return Cmd;
        }

        /// <summary>
        /// -get version: 返回Ready结果，true代表已经ready，false代表没有ready
        /// </summary>
        /// <returns></returns>
        public bool RecvVersion(byte[] data)
        {
            int index = System.Text.Encoding.Default.GetString(data).Length;

            if (index >= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// -reset: Sets the detector  back to default  settings.  
        /// This  command  takes about two seconds, plus half a second per module. 
        /// A possibly running acquisition  is stopped. 
        /// 
        /// </summary>
        /// <returns></returns>
        public byte[] Reset()
        {
            string Str = "-reset";

            byte[] Cmd = null;

            try
            {
                Cmd = System.Text.Encoding.ASCII.GetBytes(Str);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
            }

            return Cmd;
        }

        /// <summary>
        /// -nmodules n: Sets the  number  of  active modules (NMOD).   
        /// All modules are set back to default settings. 
        /// After initialization the number of active modules is set to the number  of connected modules.
        /// 
        /// </summary>
        /// <returns></returns>
        public byte[] SetActiveModules(int number)
        {
            string Str = "-nmodules " + number.ToString();

            byte[] Cmd = null;

            try
            {
                Cmd = System.Text.Encoding.ASCII.GetBytes(Str);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
            }

            return Cmd;
        }

        /// <summary>
        /// -settings name: name = Cu | Mo | Cr | Ag
        /// Loads predefined settings for the selected modules 
        /// and the specified X-ray radiation. 
        /// The energy threshold is set to a suitable value 
        /// depending on the module type (for more details 
        /// consult the User Manual). The availability of Cr 
        /// and Ag settings depends on the module type. 
        /// This command takes about half a second per module. 
        /// After initialization settings for Cu X-rays are loaded.
        /// </summary>
        /// <returns></returns>
        public byte[] LoadSettings(string name)
        {
            string Str = "-settings " + name;

            byte[] Cmd = null;

            try
            {
                Cmd = System.Text.Encoding.ASCII.GetBytes(Str);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
            }

            return Cmd;
        }

        /// <summary>
        /// -kthreshenergy f1 f2: Sets the energy threshold and the X-ray energy 
        /// for the selected modules. The supported ranges depend on the 
        /// module type. This command takes about half a second per module.
        /// Thrmin ≤ f1 ≤ Thrmax, Emin ≤ f2 ≤ Emax in units of keV. 
        /// Defaults are f1 = 6.4 keV and f2 = 8.05 keV.
        /// </summary>
        /// <returns></returns>
        public byte[] SetThresholdEnergy(double f1, double f2)
        {
            string Str = "-kthreshenergy " + f1.ToString() + " " + f2.ToString();

            byte[] Cmd = null;

            try
            {
                Cmd = System.Text.Encoding.ASCII.GetBytes(Str);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
            }

            return Cmd;
        }

        /// <summary>
        /// -readout n: Returns the number of detected photons 
        /// in each detector channel. The DCS4 can internally buffer a large 
        /// number of frames. This command returns the data of the n oldest 
        /// frames in the buffer (i.e. the frames are returned in the same 
        /// order as they were acquired). If no argument is specified, 
        /// one frame is read out. If there is an ongoing measurement and 
        /// the n requested frames are not yet ready, the server sends the 
        /// data as soon as the frames get available. If the readout fails 
        /// for some reason, all count values are set to -1. If the bad channel 
        /// interpolation is disabled, bad channels get a value of -2.
        /// </summary>
        /// <returns></returns>
        public byte[] ReadOut(int number)
        {
            string Str = "-readout " + number.ToString();

            byte[] Cmd = null;

            try
            {
                Cmd = System.Text.Encoding.ASCII.GetBytes(Str);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
            }

            return Cmd;
        }

        /// <summary>
        /// -nbits n: n = 4 | 8 | 16 | 24
        /// Sets the number of bits to be read out, thereby 
        /// determining the dynamic range and the readout time. 
        /// After initialization 24 bits are read out.
        /// </summary>
        /// <returns></returns>
        public byte[] SetBits(int number)
        {
            string Str = "-nbits " + number.ToString();

            byte[] Cmd = null;

            try
            {
                Cmd = System.Text.Encoding.ASCII.GetBytes(Str);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
            }

            return Cmd;
        }

        /// <summary>
        /// -frames n: Number of frames. Default is 1.
        /// Sets the number of frames within an acquisition. 
        /// For acquisitions with multiple frames, the programmed sequence 
        /// must not exceed the maximal frame rate.
        /// </summary>
        /// <returns></returns>
        public byte[] SetNumberOfFrames(Int64 number)
        {
            string Str = "-frames " + number.ToString();

            byte[] Cmd = null;

            try
            {
                Cmd = System.Text.Encoding.ASCII.GetBytes(Str);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
            }

            return Cmd;
        }

        /// <summary>
        /// -time time: Exposure time in units of 100 ns. Default is 1 s.
        /// Sets the number of frames within an acquisition. 
        /// For acquisitions with multiple frames, the programmed sequence 
        /// must not exceed the maximal frame rate.
        /// </summary>
        /// <returns></returns>
        public byte[] SetExposureTimeOfOneFrame(Int64 time)
        {
            string Str = "-time " + time.ToString();

            byte[] Cmd = null;

            try
            {
                Cmd = System.Text.Encoding.ASCII.GetBytes(Str);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
            }

            return Cmd;
        }

        /// <summary>
        /// -start: Starts an acquisition with the programmed number 
        /// of frames or gates.
        /// </summary>
        /// <returns></returns>
        public byte[] Start()
        {
            string Str = "-start";

            byte[] Cmd = null;

            try
            {
                Cmd = System.Text.Encoding.ASCII.GetBytes(Str);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
            }

            return Cmd;
        }

        /// <summary>
        /// -Stop: Stops the current acquisition. If the acquisition is 
        /// stopped during an ongoing exposure, the counter values at 
        /// that time can be read out with a subsequent readout command.
        /// </summary>
        /// <returns></returns>
        public byte[] Stop()
        {
            string Str = "-stop";

            byte[] Cmd = null;

            try
            {
                Cmd = System.Text.Encoding.ASCII.GetBytes(Str);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
            }

            return Cmd;
        }

        /// <summary>
        /// -get status: Returns the status of the DCS4 as a bit pattern:
        /// Bit 0: Acquisition running
        /// Bit 3: Acquisition running but exposure inactive
        /// Bit 16: No data available for readout (empty buffer)
        /// </summary>
        /// <returns></returns>
        public byte[] GetStatus()
        {
            string Str = "-get status";

            byte[] Cmd = null;

            try
            {
                Cmd = System.Text.Encoding.ASCII.GetBytes(Str);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
            }

            return Cmd;
        }

        /// <summary>
        /// -badchannelinterpolation b: Turns on or off the interpolation routine 
        /// for bad channels. When enabled, the number of counts for a bad channel 
        /// is set to the average number of counts of the next lower and the next 
        /// upper working channels. If there is no next lower (upper) working channel, 
        /// the number of counts of the next upper (lower) working channel is used. 
        /// When disabled the number of counts for a bad channel is set to -2.
        /// </summary>
        /// <returns></returns>
        public byte[] SetInterpolation(bool on)
        {
            int b = 0;
            if (on)
            {
                b = 1;
            }
            string Str = "-badchannelinterpolation" + " " + b.ToString();

            byte[] Cmd = null;

            try
            {
                Cmd = System.Text.Encoding.ASCII.GetBytes(Str);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
            }

            return Cmd;
        }

        /// <summary>
        /// -get badchannelinterpolation: Returns whether the bad 
        /// channel interpolation is enabled (1) or not (0).
        /// </summary>
        /// <returns></returns>
        public byte[] GetInterpolation()
        {
            string Str = "-get badchannelinterpolation";

            byte[] Cmd = null;

            try
            {
                Cmd = System.Text.Encoding.ASCII.GetBytes(Str);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
            }

            return Cmd;
        }

        /// <summary>
        /// -ratecorrection b: Enables or disables the rate correction. 
        /// After initialization the rate correction is disabled.
        /// </summary>
        /// <returns></returns>
        public byte[] SetRateCorrection(bool on)
        {
            int b = 0;
            if (on)
            {
                b = 1;
            }
            string Str = "-ratecorrection " + b.ToString();

            byte[] Cmd = null;

            try
            {
                Cmd = System.Text.Encoding.ASCII.GetBytes(Str);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
            }

            return Cmd;
        }

        /// <summary>
        /// -get ratecorrection: Returns whether 
        /// the rate correction is enabled (1) or not (0).
        /// </summary>
        /// <returns></returns>
        public byte[] GetRateCorrection()
        {
            string Str = "-get ratecorrection";

            byte[] Cmd = null;

            try
            {
                Cmd = System.Text.Encoding.ASCII.GetBytes(Str);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
            }

            return Cmd;
        }

        /// <summary>
        /// -trigen b: Enables or disables the single trigger mode. 
        /// The acquisition starts after the trigger signal. 
        /// Subsequent frames are started automatically. 
        /// For acquisitions with multiple frames, the programmed 
        /// sequence must not exceed the maximum frame rate.
        /// </summary>
        /// <returns></returns>
        public byte[] SetSingleTrigger(bool on)
        {
            int b = 0;
            if (on)
            {
                b = 1;
            }
            string Str = "-trigen " + b.ToString();

            byte[] Cmd = null;

            try
            {
                Cmd = System.Text.Encoding.ASCII.GetBytes(Str);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
            }

            return Cmd;
        }

        /// <summary>
        /// -conttrigen b: Enables or disables the continuous trigger mode. 
        /// In this mode, each frame requires a trigger signal. 
        /// For acquisitions with multiple frames, the programmed sequence 
        /// must not exceed the maximum frame rate.
        /// </summary>
        /// <returns></returns>
        public byte[] SetContinuousTrigger(bool on)
        {
            int b = 0;
            if (on)
            {
                b = 1;
            }
            string Str = "-conttrigen " + b.ToString();

            byte[] Cmd = null;

            try
            {
                Cmd = System.Text.Encoding.ASCII.GetBytes(Str);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
            }

            return Cmd;
        }

        /// <summary>
        /// -delbef time: Delay in units of 100 ns. Default is 0 ns. 
        /// Sets the delay between a trigger signal and the start of 
        /// the measurement (“delay before frame”). For acquisitions 
        /// with multiple frames, the programmed sequence must not 
        /// exceed the maximum frame rate. Only relevant for the single 
        /// and the continuous trigger modes.
        /// </summary>
        /// <returns></returns>
        public byte[] SetDelayBeforeFrame(int time)
        {
            string Str = "-delbef " + time.ToString();

            byte[] Cmd = null;

            try
            {
                Cmd = System.Text.Encoding.ASCII.GetBytes(Str);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
            }

            return Cmd;
        }

        /// <summary>
        /// -get delbef: Returns the delay between 
        /// a trigger signal and the start of the measurement.
        /// </summary>
        /// <returns></returns>
        public byte[] GetDelayBeforeFrame()
        {
            string Str = "-get delbef";

            byte[] Cmd = null;

            try
            {
                Cmd = System.Text.Encoding.ASCII.GetBytes(Str);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
            }

            return Cmd;
        }

        /// <summary>
        /// -delafter time: Delay in units of 100 ns. Default is 0 ns. 
        /// Sets the delay between two subsequent frames (“delay after frame”). 
        /// Setting the delay below the readout time for the current settings 
        /// has no effect. When doing this, the effective time between two 
        /// subsequent frames is equal to the readout time. For acquisitions 
        /// with multiple frames, the programmed sequence must not exceed the 
        /// maximal frame rate. Only relevant for the internal and the single trigger mode.
        /// </summary>
        /// <returns></returns>
        public byte[] SetDelayAfterFrame(int time)
        {
            string Str = "-delafter " + time.ToString();

            byte[] Cmd = null;

            try
            {
                Cmd = System.Text.Encoding.ASCII.GetBytes(Str);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
            }

            return Cmd;
        }

        /// <summary>
        /// -get delafter: Returns the programmed delay between two subsequent frames. 
        /// If the programmed delay is below the readout time for the current settings, 
        /// the effective time between two subsequent frames is equal to the readout time.
        /// </summary>
        /// <returns></returns>
        public byte[] GetDelayAfterFrame()
        {
            string Str = "-get delafter";

            byte[] Cmd = null;

            try
            {
                Cmd = System.Text.Encoding.ASCII.GetBytes(Str);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
            }

            return Cmd;
        }

        /// <summary>
        /// -log start: Instructs the socket server to log its 
        /// activities into a file on the DCS4. Do not enable 
        /// logging except for debugging purposes, since it will 
        /// slow down the server and because there is only limited 
        /// space for the log file on the DCS4.
        /// </summary>
        /// <returns></returns>
        public byte[] LogStart()
        {
            string Str = "-log start";

            byte[] Cmd = null;

            try
            {
                Cmd = System.Text.Encoding.ASCII.GetBytes(Str);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
            }

            return Cmd;
        }

        /// <summary>
        /// -log stop: Stops the logging functionality of the socket server. 
        /// The command returns the size of the log file.
        /// </summary>
        /// <returns></returns>
        public byte[] LogStop()
        {
            string Str = "-log stop";

            byte[] Cmd = null;

            try
            {
                Cmd = System.Text.Encoding.ASCII.GetBytes(Str);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
            }

            return Cmd;
        }

        /// <summary>
        /// -log status: Returns whether the logging is running (1) or not (0).
        /// </summary>
        /// <returns></returns>
        public byte[] LogStatus()
        {
            string Str = "-log status";

            byte[] Cmd = null;

            try
            {
                Cmd = System.Text.Encoding.ASCII.GetBytes(Str);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
            }

            return Cmd;
        }

        /// <summary>
        /// -log read: Instructs the socket server to send back 
        /// the content of the log file. The length of the server 
        /// response has to be inferred from the return value of the "-log stop" command.
        /// </summary>
        /// <returns></returns>
        public byte[] LogRead()
        {
            string Str = "-log read";

            byte[] Cmd = null;

            try
            {
                Cmd = System.Text.Encoding.ASCII.GetBytes(Str);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
            }

            return Cmd;
        }

        /// <summary>
        /// -testpattern: Returns a test data set with the number of counts 
        /// for each channel equal to the channel number. Can be used to 
        /// verify the client implementation of the readout mechanism.
        /// </summary>
        /// <returns></returns>
        public byte[] TestPattern()
        {
            string Str = "-testpattern";

            byte[] Cmd = null;

            try
            {
                Cmd = System.Text.Encoding.ASCII.GetBytes(Str);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
            }

            return Cmd;
        }





    }
}
