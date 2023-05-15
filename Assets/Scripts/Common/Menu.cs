using System.Linq;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public struct menuLineStruct{
        public menuLineStruct(string id, string displayName, string function){
            this._id             = id            ;
            this._displayName    = displayName   ;
            this._function       = function      ;
        }
        public string _id           {get; internal set;}
        public string _displayName  {get; internal set;}
        public string _function     {get; internal set;}
    }
    public struct menuStruct{
        public menuStruct(string id, string menuName, menuLineStruct[] menuLines) : this(){
            _id         = id        ;
            _menuName   = menuName  ;
            foreach (var line in menuLines.Select((value, i) => new { i, value })){
                _menuLines[line.i]._id          =  line.value._id           ;
                _menuLines[line.i]._displayName =  line.value._displayName  ;
                _menuLines[line.i]._function    =  line.value._function     ;
            }

        }
        public string _id {get; }
        public string _menuName {get; }
        public menuLineStruct[] _menuLines {get; }
        public override string ToString() => $"is = {_id} || menu nema = {_menuName}";
    };
    int[] int1 = {1, 2, 3, 4};
    menuStruct[] test = new menuStruct[]{
        new menuStruct("MnuMain", "Menu Principal", new menuLineStruct[]{ new menuLineStruct("10", "1011", "10aa"),
                                                        new menuLineStruct("11", "1011", "10aa")}), 
        new menuStruct("MnuSettings", "Definições", new menuLineStruct[]{ new menuLineStruct("20", "2022", "20bb"),
                                                        new menuLineStruct("20", "2022", "20bb")})
    };  
}
