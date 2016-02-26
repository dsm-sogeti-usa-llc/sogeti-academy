namespace Sogeti.Academy.Infrastructure.Models
{
	public interface IModel<TKey>
	{
		TKey Id { get; set; }
	}
}