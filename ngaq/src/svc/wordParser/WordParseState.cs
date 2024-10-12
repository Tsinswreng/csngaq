namespace ngaq.svc.wordParser;

public enum WordParseState{
	Start
	,End
	,Metadata
	,TopSpace
	,S_Date // S_ : start
	,E_Date // E_ : end
	,S_Bracket
	,E_Bracket
	,S_Brace
	,E_Brace
	,S_AngleBracket
	,E_AngleBracket
}