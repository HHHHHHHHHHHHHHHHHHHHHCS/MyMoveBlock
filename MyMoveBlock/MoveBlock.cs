using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMoveBlock
{
    public class MoveBlock
    {
        public const double sqrG2c2 = 0.5;//平方的根号2除以2

        int width,sx, sy,ex, ey;

        int kx, ky, c,d;
        double halfWidth, sqrMaxDistance;

        public void Start()
        {
            Console.WriteLine("请输入区块尺寸:");
            string s1 = Console.ReadLine();
            width = int.Parse(s1);


            Console.WriteLine("起始点:xxxx,yyyy");
            string s2 = Console.ReadLine();
            var cacheS2 = s2.Split(',');
            sx = int.Parse(cacheS2[0]);
            sy = int.Parse(cacheS2[1]);

            Console.WriteLine("结束点:xxxx,yyyy");
            string s3 = Console.ReadLine();
            var cacheS3 = s3.Split(',');
            ex = int.Parse(cacheS3[0]);
            ey = int.Parse(cacheS3[1]);


            sqrMaxDistance = width* width* sqrG2c2 ;
            halfWidth = width / 2d;

            DateTime dt1 = System.DateTime.Now;
            var list = GetList();
            DateTime dt2 = System.DateTime.Now;

            TimeSpan ts = dt2.Subtract(dt1);
            Console.WriteLine("example1 time {0}", ts.TotalMilliseconds);

            foreach (var item in list)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine(list.Count);
        }

        private void CalcLine()
        {
            kx = ey - sy;
            ky = -ex + sx;
            c = -sx * kx - sy * ky;
            d = kx * kx+ ky * ky;
        }

        private double SqrPointToLine(double x, double y)
        {
            double temp = (kx * x + ky * y + c);
            temp *= temp;
            return temp / d;
        }

        private List<KeyValuePair<int, int>> GetList()
        {
            
            CalcLine();
            List<KeyValuePair<int, int>> list = new List<KeyValuePair<int, int>>();

            int sBlockX, sBlockY, eBlockX, eBlockY;

            sBlockX = sx / width;
            sBlockY = sy / width;
            eBlockX = ex / width;
            eBlockY = ey / width;

            for(int i= sBlockX;i<=eBlockX;i++)
            {
                for (int j = sBlockY; j <= eBlockY; j++)
                {
                    double posX = i * width + halfWidth, posY = j * width + halfWidth;
                    if(SqrPointToLine(posX,posY) <= sqrMaxDistance)
                    {
                        list.Add(new KeyValuePair<int, int>( i, j));
                    }
                }
            }
            return list;

        }
    }
}
