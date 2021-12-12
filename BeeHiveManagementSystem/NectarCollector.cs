﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHiveManagementSystem
{
    public class NectarCollector : Bee
    {
        private const float NECTAR_COLLECTED_PER_SHIFT = 33.25f;
        public override float CostPerShift { get =>  1.95f; }
        public NectarCollector() : base("Nectar Collector") { }
        protected override void DoJob()
        {
            HoneyVault.CollectNectar(NECTAR_COLLECTED_PER_SHIFT);
        }
    }
}
