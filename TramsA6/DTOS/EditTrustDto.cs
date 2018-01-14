using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TramsA6.DTOS
{
    public class EditTrustDto
    {
        [Range(1, 5)]
        [DefaultValue(0)]
        public double Trust { get; set; }
    }
}