using System;

namespace Eatech.FleetManager.ApplicationCore.Entities
{
    public class Car
    {
        public Guid Id { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }

        private DateTime inspection_date;
        public DateTime Inspection_date
        {
            get
            {
                return this.inspection_date;
            }
            set
            {
                this.inspection_date = value;
            }
        }
        public int Engine_size { get; set; }

        public int Engine_power { get; set; }

        public string License_number { get; set; }

        public int ModelYear { get; set; }

    }
}