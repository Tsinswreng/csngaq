using ngaq.Core.Model.wordIF;

namespace ngaq.Core.Svc.Crud.WordCrud.IF;

public interface I_SeekFullWordKVByIdAsy {
	Task<I_FullWordKv?> SeekFullWordKVByIdAsy(i64 id);
}