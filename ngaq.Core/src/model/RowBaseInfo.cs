using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ngaq.Core.Model;

using Id_t = str;
/*
how to export type ?
 */

public class RowBaseInfo : I_RowBaseInfo {
	[Key]

	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public i64 id { get; set; }

	public str? status{get;set;}
	public str? bl { get; set; }
	//註解不可被子類繼承
	[DefaultValue("(strftime('%s', 'now') || substr(strftime('%f', 'now'), 4))")]
	public i64 ct { get; set; } = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
	[DefaultValue("(strftime('%s', 'now') || substr(strftime('%f', 'now'), 4))")]
	public i64 ut { get; set; } = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
}

//It is just difficult at the beginning
