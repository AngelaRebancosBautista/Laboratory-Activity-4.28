using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratory_Activity_28
{
    internal class Program
    {
        class BudgetPlan
        {
            private double total;
            private int days;
            private Dictionary<string, double> weights;
            private Dictionary<string, double> minima;

            public BudgetPlan(double total, int days,
                Dictionary<string, double> weights,
                Dictionary<string, double> minima)
            {
                this.total = total;
                this.days = days;
                this.weights = weights;
                this.minima = minima;
            }

            public void Allocate()
            {
                Dictionary<string, double> allocation = new Dictionary<string, double>();
                foreach (var kv in weights)
                {
                    allocation[kv.Key] = total * (kv.Value / 100.0);
                }

                foreach (var kv in minima)
                {
                    if (allocation.ContainsKey(kv.Key) && allocation[kv.Key] < kv.Value)
                    {
                        double diff = kv.Value - allocation[kv.Key];
                        allocation[kv.Key] = kv.Value;

                        if (allocation.ContainsKey("misc"))
                            allocation["misc"] -= diff;
                    }
                }

                Console.WriteLine("Travel Budget Plan");
                foreach (var kv in allocation)
                {
                    Console.WriteLine($"{kv.Key}: total {kv.Value:F2}, per-day {(kv.Value / days):F2}");
                }
                Console.WriteLine($"Total: {total}");
            }
        }

        static void Main()
        {
            double total = 1000;
            int days = 5;

            var weights = new Dictionary<string, double>
        {
            {"stay", 40},
            {"transport", 30},
            {"food", 20},
            {"misc", 10}
        };

            var minima = new Dictionary<string, double>
        {
            {"food", 150},
            {"transport", 200}
        };

            BudgetPlan plan = new BudgetPlan(total, days, weights, minima);
            plan.Allocate();
        }
    }
}
