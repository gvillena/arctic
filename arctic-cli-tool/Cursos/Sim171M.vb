Imports System.IO
Imports System.Text.RegularExpressions
Imports Newtonsoft.Json

Public Class Sim171M




    Public Shared Sub InitSmartEvalP1()

        Console.WriteLine()
        Console.WriteLine(" ------------------------")
        Console.WriteLine("| PRACTICA 01            |")
        Console.WriteLine("| SMART EVALUATION MODE  |")
        Console.WriteLine(" ------------------------")
        Console.WriteLine()

        Dim serializer As New JsonSerializer() With {.Formatting = Formatting.Indented}
        Dim path As String = IO.Path.Combine(My.Application.Info.DirectoryPath, "sim171-lst-json.txt")
        Dim stdlst = Sim171M.GetStudents()

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

        'Dim pdata = GetQuestionData(Evaluation.P1)

        'Do
        '    Dim std As New Student()
        '    With std
        '        .Id = ""
        '        .Name = ""
        '        .Grades.Add(Evaluation.P1, 0)
        '        .Grades.Add(Evaluation.EP, 0)
        '        .Grades.Add(Evaluation.P2, 0)
        '        .Grades.Add(Evaluation.EF, 0)
        '    End With
        '    Console.Write(" CODIGO: ")
        '    std.Id = Console.ReadLine()

        '    Console.Write(" NOMBRE: ")
        '    std.Name = Console.ReadLine()

        '    Console.WriteLine()

        '    Dim grade As Double = 0
        '    Dim score As Double = 0
        '    Dim ans
        '    Dim ansStr

        '    Dim isValidInput = Function(x)
        '                           If Not Regex.IsMatch(x, "^[01]{4,4}$") Then
        '                               Console.WriteLine(" PATRON DE RESPUESTA NO VALIDO, INTENTALO DE NUEVO.")
        '                               Console.WriteLine()
        '                               Return False
        '                           End If
        '                           Return True
        '                       End Function

        '    Do
        '        grade = 0
        '        For Each preg In pdata
        '            Do
        '                Console.Write(" " & preg.Name & ControlChars.Tab & "  :   ")
        '                ansStr = Console.ReadLine()
        '            Loop While (Not isValidInput(ansStr))

        '            ans = Convert.ToInt32(ansStr, 2)
        '            grade += IIf(ans = preg.Answer, preg.Score, 0)
        '            score = IIf(ans = preg.Answer, preg.Score, 0)
        '            Console.WriteLine(" SCORE" & ControlChars.Tab & "  :   " & score)
        '            Console.WriteLine()
        '            Console.Write(ControlChars.Cr)
        '        Next
        '        Console.WriteLine()
        '        Console.WriteLine(std.Name)
        '        Console.WriteLine("NOTA EXAMEN TEORICO: " & grade)
        '        Console.Write("CONFIRMAR EVALUACION (SI/NO): ")
        '    Loop While (Console.ReadLine().Trim().ToUpper() = "NO")

        '    std.Grades.Item(Evaluation.P1) = grade
        '    stdlst.Add(std)

        '    Using sw As New StreamWriter(path), writer As New JsonTextWriter(sw)
        '        serializer.Serialize(writer, stdlst)
        '    End Using

        '    Console.WriteLine()
        '    Console.Write("FINALIZAR INGRESO DE NOTAS (SI/NO): ")
        'Loop While (Console.ReadLine().Trim().ToUpper() = "NO")

    End Sub

    Friend Shared Sub InitSmartEvalEP()
        Throw New NotImplementedException()
    End Sub

    Friend Shared Sub InitSmartEvalP2()

        Console.WriteLine(" ------------------------")
        Console.WriteLine("| PRACTICA 02            |")
        Console.WriteLine("| SMART EVALUATION MODE  |")
        Console.WriteLine(" ------------------------")
        Console.WriteLine()

        Dim serializer As New JsonSerializer() With {.Formatting = Formatting.Indented}
        Dim path As String = IO.Path.Combine(My.Application.Info.DirectoryPath, "sim171-lst-json.txt")
        Dim stdlst = Sim171M.GetStudents()

        Console.Write("INGRESE CODIGO: ")
        Dim id As String = Console.ReadLine()

        Dim std As Student = stdlst.Find(Function(x) x.Id = id)
        If std Is Nothing Then
            Console.WriteLine("CODIGO NO ENCONTRADO...")
            Return
        End If

        Console.WriteLine(std.Name)

        Dim grade As Double = 0
        Dim score As Double = 0
        Dim ans
        Dim ansStr

        Dim pdata = GetQuestionData(Evaluation.P2)

        Dim isValidInput = Function(x)
                               If Not Regex.IsMatch(x, "^[01]{4,4}$") Then
                                   Console.WriteLine(" PATRON DE RESPUESTA NO VALIDO, INTENTALO DE NUEVO.")
                                   Console.WriteLine()
                                   Return False
                               End If
                               Return True
                           End Function

        Do
            grade = 0
            For Each preg In pdata
                Do
                    Console.Write(preg.Name & ControlChars.Tab & "  :   ")
                    ansStr = Console.ReadLine()
                Loop While (Not isValidInput(ansStr))

                ans = Convert.ToInt32(ansStr, 2)
                grade += IIf(ans = preg.Answer, preg.Score, 0)
                score = IIf(ans = preg.Answer, preg.Score, 0)
                Console.WriteLine("SCORE" & ControlChars.Tab & "  :   " & score)
                Console.WriteLine()
                Console.Write(ControlChars.Cr)
            Next
            Console.WriteLine(std.Name)
            Console.WriteLine("NOTA EXAMEN TEORICO: " & grade)
            Console.Write("CONFIRMAR EVALUACION (SI/NO): ")
        Loop While (Console.ReadLine().Trim().ToUpper() = "NO")



        Using sw As New StreamWriter(path), writer As New JsonTextWriter(sw)
            serializer.Serialize(writer, stdlst)
        End Using

    End Sub

    Friend Shared Sub InitSmartEvalEF()
        Throw New NotImplementedException()
    End Sub

    Public Shared Function GetQuestionData(evaluation As Evaluation) As ArrayList

        Dim pdata As ArrayList = Nothing

        Select Case evaluation

            Case Evaluation.P1
                ' TODO Evaluation.P1
                ' TODO Evaluation.P1 schema
                ' TODO Evaluation.P1 modificaciones preguntas compuestas

                Dim p1 = New With {Key .Name = "P1", .Answer = Alternativa.B, .Score = 1, .Num = 1}

                Dim p2_1 = New With {Key .Name = "P2.1", .Answer = Alternativa.C, .Score = 1, .Num = 2}
                Dim p2_2 = New With {Key .Name = "P2.2", .Answer = Alternativa.Proposito, .Score = 0.6, .Num = 3}

                Dim p3_1 = New With {Key .Name = "P3.1", .Answer = Alternativa.D, .Score = 1, .Num = 4}
                Dim p3_2 = New With {Key .Name = "P3.2", .Answer = Alternativa.Ventaja, .Score = 0.6, .Num = 5}

                Dim p4_1 = New With {Key .Name = "P4.1", .Answer = Alternativa.D, .Score = 1, .Num = 6}
                Dim p4_2 = New With {Key .Name = "P4.2", .Answer = Alternativa.Desventaja, .Score = 0.6, .Num = 7}

                Dim p5_1 = New With {Key .Name = "P5.1", .Answer = Alternativa.NoAnswer, .Score = 1, .Num = 8}
                Dim p5_2 = New With {Key .Name = "P5.2", .Answer = Alternativa.NoAnswer, .Score = 0.6, .Num = 9}

                Dim p6_1 = New With {Key .Name = "P6.1", .Answer = Alternativa.C, .Score = 1, .Num = 10}
                Dim p6_2 = New With {Key .Name = "P6.2", .Answer = Alternativa.OtrasCons, .Score = 0.6, .Num = 11}

                Dim p7_1 = New With {Key .Name = "P7.1", .Answer = Alternativa.D, .Score = 1, .Num = 12}
                Dim p7_2 = New With {Key .Name = "P7.2", .Answer = Alternativa.Proposito, .Score = 0.6, .Num = 13}

                Dim p8 = New With {Key .Name = "P8", .Answer = Alternativa.A, .Score = 1, .Num = 14}
                Dim p9 = New With {Key .Name = "P9", .Answer = Alternativa.C, .Score = 1, .Num = 15}

                Dim p10_1 = New With {Key .Name = "P10.1", .Answer = Alternativa.B, .Score = 1, .Num = 16}
                Dim p10_2 = New With {Key .Name = "P10.2", .Answer = Alternativa.Entidad, .Score = 0.6, .Num = 17}

                Dim p11_1 = New With {Key .Name = "P11.1", .Answer = Alternativa.NoAnswer, .Score = 1, .Num = 18}
                Dim p11_2 = New With {Key .Name = "P11.2", .Answer = Alternativa.NoAnswer, .Score = 0.6, .Num = 19}

                Dim p12_1 = New With {Key .Name = "P12.1", .Answer = Alternativa.A, .Score = 1, .Num = 20}
                Dim p12_2 = New With {Key .Name = "P12.2", .Answer = Alternativa.Recurso, .Score = 0.6, .Num = 21}

                Dim p13_1 = New With {Key .Name = "P13.1", .Answer = Alternativa.C, .Score = 1, .Num = 22}
                Dim p13_2 = New With {Key .Name = "P13.2", .Answer = Alternativa.Recurso, .Score = 0.6, .Num = 23}

                Dim p14 = New With {Key .Name = "P14", .Answer = Alternativa.A Or Alternativa.C, .Score = 1, .Num = 24}

                pdata = New ArrayList({p1,
                                        p2_1, p2_2,
                                        p3_1, p3_2,
                                        p4_1, p4_2,
                                        p5_1, p5_2,
                                        p6_1, p6_2,
                                        p7_1, p7_2,
                                        p8, p9,
                                        p10_1, p10_2,
                                        p11_1, p11_2,
                                        p12_1, p12_2,
                                        p13_1, p13_2,
                                        p14})

            Case Evaluation.P2
                Dim p1 = New With {Key .Name = "P1", .Answer = Alternativa.B, .Score = 0.3, .Num = 1}

                Dim p2_1 = New With {Key .Name = "P2.1", .Answer = Alternativa.C, .Score = 0.2, .Num = 2}
                Dim p2_2 = New With {Key .Name = "P2.2", .Answer = Alternativa.Proposito, .Score = 0.1, .Num = 3}

                Dim p3_1 = New With {Key .Name = "P3.1", .Answer = Alternativa.D, .Score = 0.2, .Num = 4}
                Dim p3_2 = New With {Key .Name = "P3.2", .Answer = Alternativa.Ventaja, .Score = 0.1, .Num = 5}

                Dim p4_1 = New With {Key .Name = "P4.1", .Answer = Alternativa.D, .Score = 0.2, .Num = 6}
                Dim p4_2 = New With {Key .Name = "P4.2", .Answer = Alternativa.Desventaja, .Score = 0.1, .Num = 7}

                Dim p5_1 = New With {Key .Name = "P5.1", .Answer = Alternativa.NoAnswer, .Score = 0.2, .Num = 8}
                Dim p5_2 = New With {Key .Name = "P5.2", .Answer = Alternativa.NoAnswer, .Score = 0.1, .Num = 9}

                Dim p6_1 = New With {Key .Name = "P6.1", .Answer = Alternativa.C, .Score = 0.2, .Num = 10}
                Dim p6_2 = New With {Key .Name = "P6.2", .Answer = Alternativa.OtrasCons, .Score = 0.1, .Num = 11}

                Dim p7_1 = New With {Key .Name = "P7.1", .Answer = Alternativa.D, .Score = 0.2, .Num = 12}
                Dim p7_2 = New With {Key .Name = "P7.2", .Answer = Alternativa.Proposito, .Score = 0.1, .Num = 13}

                Dim p8 = New With {Key .Name = "P8", .Answer = Alternativa.A, .Score = 0.3, .Num = 14}
                Dim p9 = New With {Key .Name = "P9", .Answer = Alternativa.C, .Score = 0.3, .Num = 15}

                Dim p10_1 = New With {Key .Name = "P10.1", .Answer = Alternativa.B, .Score = 0.2, .Num = 16}
                Dim p10_2 = New With {Key .Name = "P10.2", .Answer = Alternativa.Entidad, .Score = 0.1, .Num = 17}

                Dim p11_1 = New With {Key .Name = "P11.1", .Answer = Alternativa.NoAnswer, .Score = 0.2, .Num = 18}
                Dim p11_2 = New With {Key .Name = "P11.2", .Answer = Alternativa.NoAnswer, .Score = 0.1, .Num = 19}

                Dim p12_1 = New With {Key .Name = "P12.1", .Answer = Alternativa.A, .Score = 0.2, .Num = 20}
                Dim p12_2 = New With {Key .Name = "P12.2", .Answer = Alternativa.Recurso, .Score = 0.1, .Num = 21}

                Dim p13_1 = New With {Key .Name = "P13.1", .Answer = Alternativa.C, .Score = 0.2, .Num = 22}
                Dim p13_2 = New With {Key .Name = "P13.2", .Answer = Alternativa.Recurso, .Score = 0.1, .Num = 23}

                Dim p14 = New With {Key .Name = "P14", .Answer = Alternativa.A Or Alternativa.C, .Score = 0.3, .Num = 24}

                Dim p15 = New With {Key .Name = "P15", .Answer = Alternativa.A, .Score = 0.5, .Num = 25}
                Dim p16 = New With {Key .Name = "P16", .Answer = Alternativa.D, .Score = 0.5, .Num = 26}
                Dim p17 = New With {Key .Name = "P17", .Answer = Alternativa.C, .Score = 0.5, .Num = 27}
                Dim p18 = New With {Key .Name = "P18", .Answer = Alternativa.B, .Score = 0.5, .Num = 28}
                Dim p19 = New With {Key .Name = "P19", .Answer = Alternativa.NoAnswer, .Score = 0.5, .Num = 29}
                Dim p20 = New With {Key .Name = "P20", .Answer = Alternativa.C Or Alternativa.D, .Score = 0.5, .Num = 30}
                Dim p21 = New With {Key .Name = "P21", .Answer = Alternativa.A Or Alternativa.B Or Alternativa.C Or Alternativa.D, .Score = 0.5, .Num = 31}
                Dim p22 = New With {Key .Name = "P22", .Answer = Alternativa.A Or Alternativa.B Or Alternativa.C Or Alternativa.D, .Score = 0.5, .Num = 32}
                Dim p23 = New With {Key .Name = "P23", .Answer = Alternativa.C, .Score = 0.5, .Num = 33}
                Dim p24 = New With {Key .Name = "P24", .Answer = Alternativa.B, .Score = 0.5, .Num = 34}
                Dim p25 = New With {Key .Name = "P25", .Answer = Alternativa.A, .Score = 0.5, .Num = 35}
                Dim p26 = New With {Key .Name = "P26", .Answer = Alternativa.D, .Score = 0.5, .Num = 36}

                pdata = New ArrayList({p1,
                                        p2_1, p2_2,
                                        p3_1, p3_2,
                                        p4_1, p4_2,
                                        p5_1, p5_2,
                                        p6_1, p6_2,
                                        p7_1, p7_2,
                                        p8, p9,
                                        p10_1, p10_2,
                                        p11_1, p11_2,
                                        p12_1, p12_2,
                                        p13_1, p13_2,
                                        p14, p15, p16,
                                        p17, p18, p19,
                                        p20, p21, p22,
                                        p23, p24, p25, p26})
            Case Else
                Throw New Exception("There is something wrong!") ' TODO: Not sure if is correct! 
        End Select

        Return pdata

    End Function

    Public Shared Function GetStudents() As List(Of Student)

        Dim lst As List(Of Student) = Nothing
        Dim filepath As String = Path.Combine(My.Application.Info.DirectoryPath, "sim171-lst-json.txt")
        Dim serializer As New JsonSerializer() With {.Formatting = Formatting.Indented}

        Using file As New StreamReader(filepath)
            lst = serializer.Deserialize(file, GetType(List(Of Student)))
            If lst IsNot Nothing Then
                Debug.WriteLine(" Import from file succeeded: " & lst.Count)
            Else
                Debug.WriteLine(" Import from file failed.")
            End If
        End Using

        Return lst
    End Function

End Class
