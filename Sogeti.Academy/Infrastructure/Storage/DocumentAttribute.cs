using System;

namespace Sogeti.Academy.Infrastructure.Storage
{
	[AttributeUsage(AttributeTargets.Class, Inherited = false)]
	public class DocumentAttribute : Attribute
	{
		public string DatabaseId { get; }
		public string CollectionId { get; }
		
		public DocumentAttribute(string databaseId, string collectionId)
		{
			DatabaseId = databaseId;
			CollectionId = collectionId;
		}
	}
}