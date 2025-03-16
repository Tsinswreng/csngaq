namespace ngaq.Core.dddSample.IF;

public interface I_sendEmailAsy{
	Task sendEmailAsy(
		str to
		,str from
		,str subject
		,str body
	);
}