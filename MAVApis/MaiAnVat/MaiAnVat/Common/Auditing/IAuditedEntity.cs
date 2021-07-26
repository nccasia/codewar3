using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaiAnVat.Common.Auditing
{
	public interface IAuditedEntity
	{
		DateTime CreatedAtUtc { get; set; }
		int CreatedByUserFk { get; set; }
		DateTime? ModifiedAtUtc { get; set; }
		int? ModifiedByUserFk { get; set; }
	}
}
