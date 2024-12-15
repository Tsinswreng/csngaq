using ngaq.Core.model.wordIF;

namespace ngaq.Server.svc.crud.wordCrud.IF;

public interface I_SeekJoinedWordKVById {
	Task< I_JoinedWordKV? > SeekJoinedWordKVById(i64 id);
}