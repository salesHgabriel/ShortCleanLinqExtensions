Short clean extensions


Many methods to use use project


var list = new List<string>() { "1", "2", "3" };
var listC1 = new List<int>() { 4, 5, 6, 7 };
var listC2 = new List<int>() { 4, 5, 6, 7 };

//title
"ula leila".Title() => Ula Leila

//slug
ula leila".Stug() => ula-leila

//mask
"ula leila".Mask("*", 4) => ula *****

//snake case
"UlaLeila".Snake() => ula_leila. 

//isJson
"[1,2,3]".IsJson() => true

//toJson
list.ToJson() => ["1","2","3"]

//collapse
var manyList = new List<List<int>>() { listC1, listC2 };
var listCollapsed = manyList
    .Collapse()
    .ToList();

listCollapsed => [4,5,6,7,4,5,6,7]

// paginte api
var listPagineted = list
.AsQueryable()
.Paginate(1, 10);

//paginate web mvc or razor pages (have method .links -> create paginate html with css)


Paginate api => listPagineted => {"Page":1,"Limit":10,"FirstPage":null,"LastPage":null,"Total":3,"NextPage":null,"PreviousPage":null,"Data":["1","2","3"]};

//When
bool condition = true;

var newListFiltered = listC1
    .AsQueryable()
    .When(condition, l => l.Equals(4))
    .ToList();

newListFiltered => [4]

//Diff
List<int> firstList = new List<int>() { 1, 2, 3, 4, 5 };
List<int> secondList = new List<int>() { 2, 4, 6, 8 };

IEnumerable<int> diff = firstList.Diff(secondList);
String.Join(',', diff) => 1,3,5

