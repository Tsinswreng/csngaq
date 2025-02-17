using ngaq.Core.model.wordIF;

namespace ngaq.Server.Svc.Crud.WordCrud.IF;

public interface I_SeekFullWordKVByIdAsy {
	Task< I_FullWordKv? > SeekFullWordKVByIdAsy(i64 id);
}