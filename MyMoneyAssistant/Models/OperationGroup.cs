using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MyMoneyAssistant.Models
{
    /// <summary>
    /// Группа операций
    /// </summary>
    public class OperationGroup
    {
        /// <summary>
        /// Идентификатор группы
        /// </summary>
        [Key]
        public long Id { get; set; }
        /// <summary>
        /// Имя группы
        /// </summary>
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// Список операций этой группы
        /// </summary>
        [JsonIgnore]
        public List<Operation>? Operations { get; set;} = new List<Operation>();
    }
}
