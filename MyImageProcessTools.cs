using Emgu.CV.Structure;
using Emgu.CV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrukturaObrazuSr8
{
    public partial class Form1
    {
        private void copySelective(Image<Bgr, byte> src, Image<Bgr, byte> dst, bool mask_B, bool mask_G, bool mask_R)
        {
            byte maskB = (byte)(mask_B ? 0xFF : 0x00);
            byte maskG = (byte)(mask_G ? 0xFF : 0x00);
            byte maskR = (byte)(mask_R ? 0xFF : 0x00);

            byte[,,] srcData = src.Data;
            byte[,,] dstData = dst.Data;

            for (int x = 0; x < src.Width; x++)
            {
                for (int y = 0; y < src.Height; y++)
                {
                    dstData[y, x, 0] = (byte)(srcData[y, x, 0] & maskB);
                    dstData[y, x, 1] = (byte)(srcData[y, x, 1] & maskG);
                    dstData[y, x, 2] = (byte)(srcData[y, x, 2] & maskR);
                }
            }
        }

        private void copyMono(Image<Bgr, byte> src, Image<Bgr, byte> dst)
        {
            byte[,,] srcData = src.Data;
            byte[,,] dstData = dst.Data;

            for (int x = 0; x < src.Width; x++)
            {
                for (int y = 0; y < src.Height; y++)
                {
                    int mono = srcData[y, x, 0] + srcData[y, x, 1] + srcData[y, x, 2];
                    mono /= 3;
                    for (int i = 0; i < 3; i++) dstData[y, x, i] = (byte)(mono);
                }
            }
        }

    }
}
