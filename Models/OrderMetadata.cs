using System.ComponentModel.DataAnnotations;

namespace Order_Acceptance.Models
{

    [MetadataType(typeof(OrderMetadata))]
    public partial class Order
    {
        
    }

    public class OrderMetadata
    {
        [Display(Name = "Номер заказа")]
        public int Id { get; set; }

        [Display(Name = "Город отправителя")]
        [Required(ErrorMessage = "Пожалуйста, укажите город отправителя")]
        public string SenderCity { get; set; }

        [Display(Name = "Адрес отправителя")]
        [Required(ErrorMessage = "Пожалуйста, укажите адрес отправителя")]
        public string SenderAddress { get; set; }

        [Display(Name = "Город получателя")]
        [Required(ErrorMessage = "Пожалуйста, укажите город получателя")]
        public string RecipientCity { get; set; }

        [Display(Name = "Адрес получателя")]
        [Required(ErrorMessage = "Пожалуйста, укажите адрес получателя")]
        public string RecipientAddress { get; set; }

        [Display(Name = "Вес груза (кг)")]
        [Required(ErrorMessage = "Пожалуйста, укажите вес груза")]
        public string Weight { get; set; }

        [Display(Name = "Дата забора груза")]
        [Required(ErrorMessage = "Пожалуйста, выберите дату забора груза")]
        [DataType(DataType.Date)]
        public System.DateTime PickupDate { get; set; }

        [Display(Name = "Дата создания")]
        public System.DateTime CreatedAt { get; set; }
    }
}