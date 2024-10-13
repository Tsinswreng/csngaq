namespace ngaq.svc.wordParser;

 // S_ : start
public enum WordParseState{
	Start
	,End
	,Metadata
	,TopSpace
	,S_Bracket
	,E_Bracket
	,S_Brace
	,E_Brace
	,S_AngleBracket
	,E_AngleBracket

	,DateBlock
	,DateBlock_date // [
	,Prop // [[
	,PropKey
	,PropValue
	,WordBlock //{{
	,DateBlock_TopSpace


}