using System;

namespace DapperUnitOfWorkLib.Extensions
{

        [AttributeUsage (AttributeTargets.Property)]
        public class OrderByAttribute : Attribute {
            public OrderByAttribute (bool isDesc = false) {
                IsDesc = isDesc;
            }
            /// <summary>
            /// Whether an order is IsDesc
            /// </summary>
            public bool IsDesc { get; }
        }
}