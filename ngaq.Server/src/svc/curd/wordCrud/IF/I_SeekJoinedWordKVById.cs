using ngaq.Core.model.wordIF;

namespace ngaq.Server.svc.crud.wordCrud.IF;

public interface I_SeekJoinedWordKVById {
	Task< I_FullWordKV? > SeekJoinedWordKVById(i64 id);
}