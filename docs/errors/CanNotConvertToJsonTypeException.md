# CanNotConvertToJsonTypeException

在为系统中所有的标有了 *Config* 的类型生成 *JsonSchema* 的过程中，会产生此异常。

产生的原因是，*Config* 中的参数类型在生成 *JsonSchema* 的过程中，无法映射到一个具体 *Json* 类型上，*Json* 类型包含以下几种
* string
* number
* boolean
* object
* array

分别的映射关系如下

| JsonType | .NetType |
|---|---|
| string | *string* |
| number | *int, long, float, double* |
| boolean | *bool* |
| array | *IEnumerable&lt;T>* 或其实现类 |
| object | 声明为 *class* 的非抽象类，且不实现 *IEnumerable&lt;T>* 接口 |

**注意**

为了能够获取到集合类型中的成员类型，请使用 *IEnumerable&lt;T>* 或子实现类，而不要使用 *IEnumerable* 接口及其实现类。