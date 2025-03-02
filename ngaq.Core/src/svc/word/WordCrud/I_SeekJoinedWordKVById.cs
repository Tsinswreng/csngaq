using ngaq.Core.model.wordIF;

namespace ngaq.Core.svc.crud.wordCrud.IF;

public interface I_SeekFullWordKVByIdAsy {
	Task<I_FullWordKv?> SeekFullWordKVByIdAsy(i64 id);
}