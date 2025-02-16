using ngaq.Core.model.wordIF;

namespace ngaq.Server.Svc.Crud.WordCrud.IF;

public interface I_SeekJoinedWordKVById {
	Task< I_FullWordKv? > SeekJoinedWordKVById(i64 id);
}