﻿using Models.Abstract;
using Models.FluentBuilders;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("Orders")]
    public class OrderModel : BaseModel
    {
        public int BoxId { get; set; }
        public virtual BoxModel Box { get; set; }

        public int ProductId { get; set; }
        public virtual ProductModel Product { get; set; }

        public int UserId { get; set; }
        public virtual UserModel User { get; set; }

        public string FromPlace { get; set; }

        public DateTime? FromDate { get; set; }

        public string FromTime { get; set; }

        public string ToPlace { get; set; }

        public DateTime? ToDate { get; set; }

        public string ToTime { get; set; }

        public string AvailabilityOfInsurancePurchased { get; set; } = "False";

        public string ComplianceTemperatureRegimePurchased { get; set; } = "False";

        public string PackagingPurchased { get; set; } = "False";

        public double TotalCost { get; set; }

        public OrderModel() => CreatedDate = DateTime.UtcNow;

        public static OrderBuilder CreateBuilder() => new OrderBuilder();
    }
}
