using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHiveManagementSystem
{
    public class Queen : Bee, INotifyPropertyChanged
    {
        private const float EGGS_PER_SHIFT = 0.45f;
        private const float HONEY_PER_UNASSIGNED_WORKER = 0.5f;

        private IWorker[] workers = new IWorker[0];
        private float eggs = 0;
        private float unassignedWorkers = 3;

        public event PropertyChangedEventHandler PropertyChanged;

        public string StatusReport { get; private set; }
        public override float CostPerShift { get { return 2.15f; } }

        public Queen() : base("Queen")
        {
            AssigneBee("Nectar Colleactor");
            AssigneBee("Honey Manufacturer");
            AssigneBee("Egg Care");
        }

        protected void OnPropertyChanges(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private void AddWorker(Bee worker)
        {
            if (unassignedWorkers >= 1)
            {
                unassignedWorkers--;
                Array.Resize(ref workers, workers.Length + 1);
                workers[workers.Length - 1] = worker;
            }
        }

        private void UpdateStatusReport()
        {
            StatusReport = $"Vault report:\n{HoneyVault.StatusReport}\n" +
                $"\nEgg count: {eggs:0.0}\nUnasssigned workers: {unassignedWorkers:0.0}\n" +
                $"\n{WorkerStatus("Egg Care")}\n TOTAL WORKERS: {workers.Length}";
            OnPropertyChanges("StatusReport");
        }

        public void CareForEggs(float eggsToConvert)
        {
            if (eggs >= eggsToConvert)
            {
                eggs -= eggsToConvert;
                unassignedWorkers += eggsToConvert;
            }
        }

        private string WorkerStatus(string job)
        {
            int count = 0;
            foreach (IWorker worker in workers)
            {
                if (worker.Job == job)
                {
                    count++;
                }
            }

            string s = "s";
            if (count == 1)
            {
                s = "";
            }

            return $"{count} {job} bee{s}";
        }

        public void AssigneBee(string job)
        {
            switch (job)
            {
                case "Nectar Collector":
                    AddWorker(new NectarCollector());
                    break;
                case "Honey Manufacturer":
                    AddWorker(new HoneyManufacturer());
                    break;
                case "Egg Care":
                    AddWorker(new EggCare(this));
                    break;
            }
            UpdateStatusReport();
        }

        protected override void DoJob()
        {
            eggs += EGGS_PER_SHIFT;
            foreach (Bee worker in workers)
            {
                worker.WorkTheNextShift();
            }
            HoneyVault.ConsumeHoney(unassignedWorkers * HONEY_PER_UNASSIGNED_WORKER);
            UpdateStatusReport();
        }
    }
}
