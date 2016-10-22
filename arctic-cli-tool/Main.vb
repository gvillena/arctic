Imports System.Text
Imports CommandLine
Imports CommandLine.Text
Imports Newtonsoft.Json

Module Main

    Sub Main()

        ' Declare and initialize parser 
        Dim parser = New Parser(Sub(config) config.HelpWriter = Console.Out)

        ' Parse result
        Dim args = My.Application.CommandLineArgs
        Dim result = parser.ParseArguments(Of RunOptions, ListOptions, GradeOptions, TestOptions)(args)

        ' Execute parsed action
        result.WithParsed(Of RunOptions)(AddressOf Run).
                WithParsed(Of ListOptions)(AddressOf List).
                WithParsed(Of GradeOptions)(AddressOf Grade).
                WithParsed(Of TestOptions)(AddressOf Test).
                WithNotParsed(Function(errors) 1)

    End Sub

    Function Run(opts As RunOptions)
        Return 0
    End Function

    Function List(opts As ListOptions)

        Dim sb = New StringBuilder()
        Dim stdLst = StudentIO.GetStudents("sim162-lst.txt")

        sb.AppendLine()
        sb.AppendLine(" " & HeadingInfo.Default.ToString)
        sb.AppendLine()

        sb.AppendFormat("  {0,-10}  {1, -40} {2}", "Id", "Name", vbCrLf)

        For Each std As Student In stdLst
            sb.AppendFormat("  {0,-10}  {1, -40} {2}", std.Id, std.Name, vbCrLf)
        Next

        Console.WriteLine()
        Console.WriteLine(sb.ToString())
        Return 0
    End Function

    Function Grade(opts As GradeOptions)

        Console.WriteLine()
        Console.WriteLine("Calculate Grade Test v0.2")
        Console.WriteLine("-------------------------")
        Console.WriteLine()

        Console.Write("Codigo Alumno: ")
        Dim id = Console.ReadLine()

        Dim lstFileName = "sim162-lst.txt"
        Dim std = StudentIO.GetStudent(id, lstFileName)
        If std Is Nothing Then
            Console.WriteLine()
            Console.WriteLine("Opss :( no pude encontrar el codigo " & id & " en la lista " & lstFileName)
            Console.WriteLine("Verifica que todo este bien e intentalo de nuevo... ;)")
            Return 0
        End If

        Console.WriteLine(std.Name & Environment.NewLine)

        Dim grde = StudentIO.CalcGrade(Course.sim162, Evaluation.P1)
        Console.WriteLine()
        Console.WriteLine("¡Finalizado!")
        Console.WriteLine()
        Console.WriteLine()
        Console.WriteLine("Nota P1: " & grde.ToString("#0.00"))

        If grde > 10.5 Then
            Console.WriteLine("Aprobado ;)")
        Else

            Dim msgs = New ArrayList()
            With msgs
                .Add("Lo importante es que tienes salud ;)")
                .Add("Falta poco para verano ;)")
                .Add("¡A nada! ;)")
                .Add("¡Susti! ;)")
            End With

            Console.WriteLine("Sorry :(")
            Console.WriteLine(msgs(New Random().Next(3)))

        End If


        Return 0
    End Function

    Function Test(opts As TestOptions)

        Console.WriteLine()
        Console.WriteLine("¡Hello World!")

        'Dim sb = New StringBuilder()
        'Dim stdLst = StudentIO.GetStudents("sim162-lst.txt")

        'sb.AppendLine()
        'sb.AppendLine("  " & HeadingInfo.Default.ToString)
        'sb.AppendLine()

        'sb.AppendFormat("  {0,-10}  {1, -40} P1  EP  P2  EF  PF {2}", "Id", "Name", vbCrLf)

        'For Each std As Student In stdLst
        '    sb.AppendFormat("  {0,-10} {1, -40} {2:00} {}", std.Id, std.Name,
        '                                                    std.Grades(Evaluation.P1),
        '                                                    std.Grades(Evaluation.EP),
        '                                                    std.Grades(Evaluation.P2),
        '                                                    std.Grades(Evaluation.EF),
        '                                                    std.FinalGrade,
        '                                                    vbCrLf)
        'Next

        'Console.WriteLine()
        'Console.WriteLine(sb.ToString())

        'Dim stdLst = StudentIO.GetStudents("visual162-lst.txt")
        'StudentIO.SetGrades("sim162-lst.txt", "prac01.txt")
        'Dim stdLst = StudentIO.GetGrades("visual162-lst.txt")
        'Dim json = JsonConvert.SerializeObject(stdLst, Formatting.Indented)
        'Console.WriteLine(json)
        Return 0

    End Function

End Module
