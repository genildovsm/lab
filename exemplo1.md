O método `GroupJoin` em LINQ é usado para realizar uma operação de junção entre dois conjuntos de dados, semelhante ao `Join`, mas resultando em grupos de elementos do primeiro conjunto juntados com elementos correspondentes do segundo conjunto.

O método `GroupJoin` é útil quando você precisa agrupar os resultados da junção com base em uma chave comum. Ele retorna uma sequência de objetos que contêm cada elemento do primeiro conjunto e uma coleção de elementos correspondentes do segundo conjunto, agrupados pela chave comum.

Aqui está a assinatura do método `GroupJoin`:

```csharp
public static IEnumerable<TResult> GroupJoin<TOuter, TInner, TKey, TResult>(
    this IEnumerable<TOuter> outer,
    IEnumerable<TInner> inner,
    Func<TOuter, TKey> outerKeySelector,
    Func<TInner, TKey> innerKeySelector,
    Func<TOuter, IEnumerable<TInner>, TResult> resultSelector
)
```

- `outer`: O primeiro conjunto de dados (coleção) que será usado na junção.
- `inner`: O segundo conjunto de dados (coleção) que será usado na junção.
- `outerKeySelector`: Uma função que extrai a chave de junção do primeiro conjunto.
- `innerKeySelector`: Uma função que extrai a chave de junção do segundo conjunto.
- `resultSelector`: Uma função que projeta o resultado da junção para um tipo de resultado desejado.

Exemplo:

Considere duas listas de objetos, uma representando estudantes e outra representando suas notas:

```csharp
class Student
{
    public int Id { get; set; }
    public string Name { get; set; }
}

class Grade
{
    public int StudentId { get; set; }
    public double Score { get; set; }
}
```

Agora, vamos realizar uma junção de grupo (GroupJoin) entre essas duas listas:

```csharp
var students = new List<Student>
{
    new Student { Id = 1, Name = "Alice" },
    new Student { Id = 2, Name = "Bob" },
    new Student { Id = 3, Name = "Charlie" }
};

var grades = new List<Grade>
{
    new Grade { StudentId = 1, Score = 85 },
    new Grade { StudentId = 2, Score = 90 },
    new Grade { StudentId = 1, Score = 88 },
    new Grade { StudentId = 3, Score = 95 }
};

var groupedGrades = students.GroupJoin(grades,
                                       student => student.Id,
                                       grade => grade.StudentId,
                                       (student, gradeGroup) => new
                                       {
                                           student.Name,
                                           Grades = gradeGroup.Select(g => g.Score)
                                       });

foreach (var studentGrades in groupedGrades)
{
    Console.WriteLine($"Student: {studentGrades.Name}");
    foreach (var grade in studentGrades.Grades)
    {
        Console.WriteLine($"  Grade: {grade}");
    }
    Console.WriteLine();
}
```

Neste exemplo, `students` e `grades` são duas coleções de objetos `Student` e `Grade`, respectivamente. O método `GroupJoin` é usado para agrupar os elementos de `grades` por `StudentId` e, em seguida, os resultados são projetados em uma nova forma de objeto que contém o nome do aluno e suas notas agrupadas.
