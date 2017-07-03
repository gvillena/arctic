Imports System.IO
Imports System.Text
Imports System.Text.RegularExpressions
Imports Newtonsoft.Json


Public Class Se171M

    Public Shared Function GetStudents() As List(Of Student)

        Dim lst As List(Of Student) = Nothing
        Dim filepath As String = Path.Combine(My.Application.Info.DirectoryPath, "se171-lst-json.txt")
        Dim serializer As New JsonSerializer() With {.Formatting = Formatting.Indented}

        Using file As New StreamReader(filepath)
            lst = serializer.Deserialize(file, GetType(List(Of Student)))
            If lst IsNot Nothing Then
                Debug.WriteLine("Import from file succeeded: " & lst.Count)
            Else
                Debug.WriteLine("Import from file failed.")
            End If
        End Using

        Return lst
    End Function

    Public Shared Sub InitGradeMngr()

        Console.WriteLine()
        Console.WriteLine(" ------------------------------ ")
        Console.WriteLine("| SISTEMAS EMPRESARIALES 2017-1 |")
        Console.WriteLine("| EVALUATION GRADES MANAGER     |")
        Console.WriteLine(" ------------------------------ ")
        Console.WriteLine()

        Dim serializer As New JsonSerializer() With {.Formatting = Formatting.Indented}
        Dim path As String = IO.Path.Combine(My.Application.Info.DirectoryPath, "se171-lst-json.txt")
        Dim stdlst = Se171M.GetStudents()

        Dim sb As New StringBuilder()
        sb.AppendLine()
        sb.AppendFormat(" {0,2}  {1, -40}  {2, -5}  {3, -5}  {4, -5}  {5, -5}  {6, -5} {7}",
                                  "N°", "NAME", "PRC01", "EXPRC", "PRC02", "EXFNL", "PROMD", vbCrLf)
        For Each std As Student In stdlst
            sb.AppendFormat(" {0,2}  {1, -40}  {2, -5}  {3, -5}  {4, -5}  {5, -5}  {6, -5} {7}",
                                      String.Format("{0:00}", stdlst.IndexOf(std) + 1),
                                      std.Name,
                                      String.Format("{0:00.00}", std.Grades(Evaluation.P1)),
                                      String.Format("{0:00.00}", std.Grades(Evaluation.EP)),
                                      String.Format("{0:00.00}", std.Grades(Evaluation.P2)),
                                      String.Format("{0:00.00}", std.Grades(Evaluation.EF)),
                                      String.Format("{0:00.00}", std.FinalGrade),
                                      vbCrLf)
        Next
        Console.WriteLine(sb.ToString)
        Console.WriteLine()

    End Sub

    Public Shared Sub InitSmartEvalP1()

        Console.WriteLine()
        Console.WriteLine(" ------------------------")
        Console.WriteLine("| PRACTICA 01            |")
        Console.WriteLine("| SMART EVALUATION MODE  |")
        Console.WriteLine(" ------------------------")
        Console.WriteLine()

        Dim serializer As New JsonSerializer() With {.Formatting = Formatting.Indented}
        Dim path As String = IO.Path.Combine(My.Application.Info.DirectoryPath, "se171-lst-json.txt")
        Dim stdlst = Se171M.GetStudents()

        Console.Write("INGRESE CODIGO: ")

        Dim id As String = Console.ReadLine()
        Dim std As Student = stdlst.Find(Function(x) x.Id = id)

        If std Is Nothing Then
            Console.WriteLine("CODIGO NO ENCONTRADO...")
            Return
        End If

        Console.WriteLine()
        Console.WriteLine("{0}", std.Name)
        Console.WriteLine("INGRESE RESPUESTAS...")
        Console.WriteLine()

        Dim pdata = GetQuestionData(Evaluation.P1)

        Dim isValidInput = Function(x)
                               If Not Regex.IsMatch(x, "^[01]{4,4}$") Then
                                   Console.WriteLine("PATRON DE RESPUESTA NO VALIDO, INTENTALO DE NUEVO.")
                                   Console.WriteLine()
                                   Return False
                               End If
                               Return True
                           End Function

        Dim grade As Double = 0

        Do
            Dim score As Double = 0
            Dim ansInt As Integer = 0
            Dim ansStr As String = String.Empty

            For Each preg In pdata
                Do
                    Console.Write(preg.Name & ControlChars.Tab & "  :   ")
                    ansStr = Console.ReadLine()
                Loop While (Not isValidInput(ansStr))
                ansInt = Convert.ToInt32(ansStr, 2)
                grade += IIf(ansInt = preg.Answer, preg.Score, 0)
                score = IIf(ansInt = preg.Answer, preg.Score, 0)
                Console.WriteLine("SCORE" & ControlChars.Tab & "  :   " & score)
                Console.WriteLine()
                Console.Write(ControlChars.Cr)
            Next
            Console.WriteLine(std.Name)
            Console.WriteLine("NOTA EXAMEN TEORICO: " & grade)
            Console.Write("CONFIRMAR EVALUACION (SI/NO): ")
        Loop While (Console.ReadLine().Trim().ToUpper() = "NO")

        std.Grades.Item(Evaluation.P1) = grade

        Using sw As New StreamWriter(path), writer As New JsonTextWriter(sw)
            serializer.Serialize(writer, stdlst)
        End Using

    End Sub

    Public Shared Sub InitSmartEvalP1()

        Console.WriteLine()
        Console.WriteLine(" ---------------------------")
        Console.WriteLine("| EXAMEN PARCIAL            |")
        Console.WriteLine("| SMART EVALUATION MODE     |")
        Console.WriteLine(" ---------------------------")
        Console.WriteLine()

        Dim serializer As New JsonSerializer() With {.Formatting = Formatting.Indented}
        Dim path As String = IO.Path.Combine(My.Application.Info.DirectoryPath, "se171-lst-json.txt")
        Dim stdlst = Se171M.GetStudents()

        Console.Write("INGRESE CODIGO: ")

        Dim id As String = Console.ReadLine()
        Dim std As Student = stdlst.Find(Function(x) x.Id = id)

        If std Is Nothing Then
            Console.WriteLine("CODIGO NO ENCONTRADO...")
            Return
        End If

        Console.WriteLine()
        Console.WriteLine("{0}", std.Name)
        Console.WriteLine("INGRESE RESPUESTAS...")
        Console.WriteLine()

        Dim pdata = GetQuestionData(Evaluation.P1)

        Dim isValidInput = Function(x)
                               If Not Regex.IsMatch(x, "^[01]{4,4}$") Then
                                   Console.WriteLine("PATRON DE RESPUESTA NO VALIDO, INTENTALO DE NUEVO.")
                                   Console.WriteLine()
                                   Return False
                               End If
                               Return True
                           End Function

        Dim grade As Double = 0

        Do
            Dim score As Double = 0
            Dim ansInt As Integer = 0
            Dim ansStr As String = String.Empty

            For Each preg In pdata
                Do
                    Console.Write(preg.Name & ControlChars.Tab & "  :   ")
                    ansStr = Console.ReadLine()
                Loop While (Not isValidInput(ansStr))
                ansInt = Convert.ToInt32(ansStr, 2)
                grade += IIf(ansInt = preg.Answer, preg.Score, 0)
                score = IIf(ansInt = preg.Answer, preg.Score, 0)
                Console.WriteLine("SCORE" & ControlChars.Tab & "  :   " & score)
                Console.WriteLine()
                Console.Write(ControlChars.Cr)
            Next
            Console.WriteLine(std.Name)
            Console.WriteLine("NOTA EXAMEN TEORICO: " & grade)
            Console.Write("CONFIRMAR EVALUACION (SI/NO): ")
        Loop While (Console.ReadLine().Trim().ToUpper() = "NO")

        std.Grades.Item(Evaluation.EP) = grade

        Using sw As New StreamWriter(path), writer As New JsonTextWriter(sw)
            serializer.Serialize(writer, stdlst)
        End Using

    End Sub

    Public Shared Function GetQuestionData(evaluation As Evaluation) As ArrayList

        Dim pdata As ArrayList = Nothing

        Select Case evaluation

            Case Evaluation.P1
                ' TODO Evaluation.P1
                ' TODO Evaluation.P1 schema
                ' TODO Evaluation.P1 modificaciones preguntas compuestas

                Dim p1 = New With {Key .Name = "P1", .Answer = Alternativa.A Or Alternativa.B Or Alternativa.C, .Score = 1.67, .Num = 1}
                Dim p2 = New With {Key .Name = "P2", .Answer = Alternativa.A, .Score = 1.65, .Num = 1}
                Dim p3 = New With {Key .Name = "P3", .Answer = Alternativa.C, .Score = 1.65, .Num = 1}
                Dim p4 = New With {Key .Name = "P4", .Answer = Alternativa.B, .Score = 1.65, .Num = 1}
                Dim p5 = New With {Key .Name = "P5", .Answer = Alternativa.B, .Score = 1.65, .Num = 1}
                Dim p6 = New With {Key .Name = "P6", .Answer = Alternativa.B, .Score = 1.65, .Num = 1}
                Dim p7 = New With {Key .Name = "P7", .Answer = Alternativa.B, .Score = 1.65, .Num = 1}
                Dim p8 = New With {Key .Name = "P8", .Answer = Alternativa.NoAnswer, .Score = 1.65, .Num = 1}
                Dim p9 = New With {Key .Name = "P9", .Answer = Alternativa.A, .Score = 1.65, .Num = 1}
                Dim p10 = New With {Key .Name = "P10", .Answer = Alternativa.B, .Score = 1.65, .Num = 1}
                Dim p11 = New With {Key .Name = "P11", .Answer = Alternativa.C, .Score = 1.65, .Num = 1}
                Dim p12 = New With {Key .Name = "P12", .Answer = Alternativa.NoAnswer, .Score = 1.65, .Num = 1}

                pdata = New ArrayList({p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12})

            Case Evaluation.EP

                Dim p1 = New With {Key .Name = "P1", .Answer = Alternativa.A Or Alternativa.B Or Alternativa.C, .Score = 0.4, .Num = 1}
                Dim p2 = New With {Key .Name = "P2", .Answer = Alternativa.A, .Score = 0.4, .Num = 1}
                Dim p3 = New With {Key .Name = "P3", .Answer = Alternativa.C, .Score = 0.4, .Num = 1}
                Dim p4 = New With {Key .Name = "P4", .Answer = Alternativa.B, .Score = 0.4, .Num = 1}
                Dim p5 = New With {Key .Name = "P5", .Answer = Alternativa.B, .Score = 0.4, .Num = 1}
                Dim p6 = New With {Key .Name = "P6", .Answer = Alternativa.B, .Score = 0.4, .Num = 1}
                Dim p7 = New With {Key .Name = "P7", .Answer = Alternativa.B, .Score = 0.4, .Num = 1}
                Dim p8 = New With {Key .Name = "P8", .Answer = Alternativa.NoAnswer, .Score = 0.4, .Num = 1}
                Dim p9 = New With {Key .Name = "P9", .Answer = Alternativa.A, .Score = 0.4, .Num = 1}
                Dim p10 = New With {Key .Name = "P10", .Answer = Alternativa.B, .Score = 0.4, .Num = 1}
                Dim p11 = New With {Key .Name = "P11", .Answer = Alternativa.C, .Score = 0.4, .Num = 1}
                Dim p12 = New With {Key .Name = "P12", .Answer = Alternativa.NoAnswer, .Score = 0.4, .Num = 1}
                Dim p13 = New With {Key .Name = "P13", .Answer = Alternativa.NoAnswer, .Score = 1.5, .Num = 1}
                Dim p14 = New With {Key .Name = "P14", .Answer = Alternativa.NoAnswer, .Score = 1.5, .Num = 1}
                Dim p15 = New With {Key .Name = "P15", .Answer = Alternativa.NoAnswer, .Score = 1.5, .Num = 1}
                Dim p16 = New With {Key .Name = "P16", .Answer = Alternativa.NoAnswer, .Score = 1.5, .Num = 1}
                Dim p17 = New With {Key .Name = "P17", .Answer = Alternativa.NoAnswer, .Score = 1.5, .Num = 1}
                Dim p18 = New With {Key .Name = "P18", .Answer = Alternativa.NoAnswer, .Score = 1.5, .Num = 1}
                Dim p19 = New With {Key .Name = "P19", .Answer = Alternativa.NoAnswer, .Score = 1.5, .Num = 1}
                Dim p20 = New With {Key .Name = "P20", .Answer = Alternativa.NoAnswer, .Score = 1.5, .Num = 1}
                Dim p21 = New With {Key .Name = "P21", .Answer = Alternativa.NoAnswer, .Score = 1.6, .Num = 1}
                Dim p22 = New With {Key .Name = "P22", .Answer = Alternativa.NoAnswer, .Score = 1.6, .Num = 1}

                pdata = New ArrayList({p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22})


            Case Else
                Throw New Exception("There is something wrong!") ' TODO: Not sure if is correct! 
        End Select

        Return pdata

    End Function

    Friend Shared Sub InitSmartEvalEP()
        Throw New NotImplementedException()
    End Sub

    Friend Shared Sub InitSmartEvalP2()
        Throw New NotImplementedException()
    End Sub

    Friend Shared Sub InitSmartEvalEF()
        Throw New NotImplementedException()
    End Sub
End Class
