using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kurisu.Game.Data
{
    public class FlightConfig
    {
        private float moveFBSpeed = 40;

        public float MoveFBSpeed
        {
            get { return moveFBSpeed; }
            set { moveFBSpeed = value; }
        }
        private float moveLRSpeed = 15;

        public float MoveLRSpeed
        {
            get { return moveLRSpeed; }
            set { moveLRSpeed = value; }
        }
        private float roteFBSpeed = 30;

        public float RoteFBSpeed
        {
            get { return roteFBSpeed; }
            set { roteFBSpeed = value; }
        }
        private float roteLRSpeed = 30;

        public float RoteLRSpeed
        {
            get { return roteLRSpeed; }
            set { roteLRSpeed = value; }
        }
        private float acc = 500;

        public float Acc
        {
            get { return acc; }
            set { acc = value; }
        }

        private float axisLR = 45;

        public float AxisLR
        {
            get { return axisLR; }
            set { axisLR = value; }
        }
        private float axisFB = 75;

        public float AxisFB
        {
            get { return axisFB; }
            set { axisFB = value; }
        }
        private float takeOffSpeed = 25f;

        public float TakeoffSpeed
        {
            get { return takeOffSpeed; }
            set { takeOffSpeed = value; }
        }
        private float stuntTime;

        public float StuntTime
        {
            get { return stuntTime; }
            set { stuntTime = value; }
        }
        private float maxSpeed = 150;

        public float MaxSpeed
        {
            get { return maxSpeed; }
            set { maxSpeed = value; }
        }
        private float minSpeed = 0;
        public float MinSpeed
        {
            get { return minSpeed; }
            set { minSpeed = value; }
        }
    }
}
