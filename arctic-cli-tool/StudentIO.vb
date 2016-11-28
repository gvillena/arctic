Imports System.IO
Imports System.Text
Imports Newtonsoft.Json

Public Class StudentIO

    Public Shared Function GetStudentFromSim162(id As String) As Student

    End Function

    Public Shared Function GetStudentFromSim162(id As String) As Student

    End Function

    Public Shared Function GetStudent(id As String, fileName As String) As Student

        Dim std As Student = Nothing

        Dim path As String = IO.Path.Combine(My.Application.Info.DirectoryPath, fileName)
        Using MyReader As New Microsoft.VisualBasic.FileIO.TextFieldParser(path)

            MyReader.TextFieldType = FileIO.FieldType.Delimited
            MyReader.SetDelimiters(vbTab)

            Dim currentRow As String()
            While Not MyReader.EndOfData
                Try
                    currentRow = MyReader.ReadFields()
                    If id = currentRow.ElementAt(1) Then
                        std = New Student()
                        With std
                            .Id = currentRow.ElementAt(1)
                            .Name = currentRow.ElementAt(2)
                            .Grades.Add(Evaluation.P1, 0)
                            .Grades.Add(Evaluation.EP, 0)
                            .Grades.Add(Evaluation.P2, 0)
                            .Grades.Add(Evaluation.EF, 0)
                        End With
                        Exit While
                    End If
                Catch ex As Microsoft.VisualBasic.FileIO.MalformedLineException
                    Console.WriteLine("Line " & ex.Message & "is not valid and will be skipped.")
                End Try
            End While
        End Using

        Return std
    End Function

    Public Shared Function GetStudents(ByVal fileName As String) As ArrayList

        Dim lst As ArrayList = New ArrayList()
        Dim std As Student = Nothing
        Dim path As String = IO.Path.Combine(My.Application.Info.DirectoryPath, fileName)

        Using MyReader As New Microsoft.VisualBasic.FileIO.TextFieldParser(path)

            MyReader.TextFieldType = FileIO.FieldType.Delimited
            MyReader.SetDelimiters(vbTab)

            Dim currentRow As String()
            While Not MyReader.EndOfData
                Try
                    currentRow = MyReader.ReadFields()
                    std = New Student()
                    With std
                        .Id = currentRow.ElementAt(1)
                        .Name = currentRow.ElementAt(2)
                        .Grades.Add(Evaluation.P1, 0)
                        .Grades.Add(Evaluation.EP, 0)
                        .Grades.Add(Evaluation.P2, 0)
                        .Grades.Add(Evaluation.EF, 0)
                    End With
                    lst.Add(std)
                Catch ex As Microsoft.VisualBasic.
                            FileIO.MalformedLineException
                    Console.WriteLine("Line " & ex.Message &
                    "is not valid and will be skipped.")
                End Try
            End While
        End Using

        'For Each s As Student In lst
        '    Console.WriteLine("{0} {1}", CStr(s.Id), s.Name)
        'Next
        Return lst
    End Function

    Public Shared Function GetStudentsFromTextFile(ByVal fileName As String) As List(Of Student)

        Dim std As Student = Nothing
        Dim path As String = IO.Path.Combine(My.Application.Info.DirectoryPath, fileName)
        Dim lst As New List(Of Student)

        Using MyReader As New Microsoft.VisualBasic.FileIO.TextFieldParser(path)


            MyReader.TextFieldType = FileIO.FieldType.Delimited
            MyReader.SetDelimiters(vbTab)

            Dim currentRow As String()
            While Not MyReader.EndOfData
                Try
                    currentRow = MyReader.ReadFields()
                    std = New Student()
                    With std
                        .Id = currentRow.ElementAt(1)
                        .Name = currentRow.ElementAt(2)
                        .Grades.Add(Evaluation.P1, 0)
                        .Grades.Add(Evaluation.EP, 0)
                        .Grades.Add(Evaluation.P2, 0)
                        .Grades.Add(Evaluation.EF, 0)
                    End With
                    lst.Add(std)
                Catch ex As Microsoft.VisualBasic.
                            FileIO.MalformedLineException
                    Console.WriteLine("Line " & ex.Message &
                    "is not valid and will be skipped.")
                End Try
            End While
        End Using

        'For Each s As Student In lst
        '    Console.WriteLine("{0} {1}", CStr(s.Id), s.Name)
        'Next
        Return lst
    End Function

    Public Shared Function GetStudentsFromJson(ByVal fileName As String) As List(Of Student)

        Dim lst As List(Of Student) = Nothing
        Dim path As String = IO.Path.Combine(My.Application.Info.DirectoryPath, fileName)
        Dim serializer As New JsonSerializer() With {.Formatting = Formatting.Indented}

        Using file As New StreamReader(path)
            lst = serializer.Deserialize(file, GetType(List(Of Student)))
            If lst IsNot Nothing Then
                Console.WriteLine("Import from file succeeded: " & lst.Count)
            Else
                Console.WriteLine("Import from file failed.")
            End If
        End Using

        Return lst

    End Function

    Public Shared Function GetStudentsJson(ByVal fileName As String) As List(Of Student)

        Dim lst As List(Of Student) = Nothing
        'Dim std As Student = Nothing
        Dim path As String = IO.Path.Combine(My.Application.Info.DirectoryPath, fileName)
        Dim serializer As New JsonSerializer() With {.Formatting = Formatting.Indented}

        Using file As New StreamReader(path)
            lst = serializer.Deserialize(file, GetType(List(Of Student)))
            If lst IsNot Nothing Then
                Console.WriteLine("Succeed " & lst.Count)
            Else
                Console.WriteLine("Nothing")
            End If
        End Using

        Return lst

    End Function


    Public Shared Sub SetGrades(stdsFileName As String, testFileName As String)

        Console.WriteLine()
        Console.Write("Codigo Alumno: ")
        Dim id = Console.ReadLine()

        Dim std = GetStudent(id, stdsFileName)

        If std Is Nothing Then
            Console.WriteLine()
            Console.WriteLine("Opss :( no pude encontrar el codigo " & id & " en la lista " & stdsFileName)
            Console.WriteLine("Verifica que todo este bien e intentalo de nuevo... ;)")
            Exit Sub
        End If

        Console.WriteLine(std.Name & Environment.NewLine)
        'Console.WriteLine("Ingrese...")

        Dim sb = New StringBuilder()
        sb.Append(id & ControlChars.Tab)

        Dim pdata = GetQuestionData(testFileName)

        For i = 0 To 3
            Console.Write("Respuesta " & pdata(i).Name & " ")
            Dim respStr = Console.ReadLine()
            sb.Append(respStr & ControlChars.Tab)
            Console.Write(ControlChars.Cr)
            'Dim resp = Convert.ToInt32(respStr, 2)
            'Console.WriteLine(resp)
        Next

        Dim path = IO.Path.Combine(My.Application.Info.DirectoryPath, testFileName)
        IO.File.AppendAllText(path, sb.ToString & Environment.NewLine)
        Console.WriteLine(sb.ToString)
        'For Each preg In pdata
        'Next

    End Sub


    Public Shared Function GetGrades(ByVal fileName As String) As ArrayList

        Dim lst As ArrayList = New ArrayList()
        Dim std As Student = Nothing
        Dim path As String = IO.Path.Combine(My.Application.Info.DirectoryPath, fileName)

        Using MyReader As New Microsoft.VisualBasic.FileIO.TextFieldParser(path)

            MyReader.TextFieldType = FileIO.FieldType.Delimited
            MyReader.SetDelimiters(vbTab)

            Dim currentRow As String()

            While Not MyReader.EndOfData
                Try
                    currentRow = MyReader.ReadFields()
                    std = New Student()
                    With std
                        .Id = currentRow.ElementAt(1)
                        .Name = currentRow.ElementAt(2)
                        .Grades.Add(Evaluation.P1, CalcGrade("2007202383", "prac01.txt"))
                        .Grades.Add(Evaluation.EP, 0)
                        .Grades.Add(Evaluation.P2, 0)
                        .Grades.Add(Evaluation.EF, 0)
                        '.Grades.Add(Evaluacion.EP, CalcGrade("2007202389", "exprcl.txt"))
                        '.Grades.Add(Evaluacion.P2, CalcGrade("2007202389", "prac02.txt"))
                        '.Grades.Add(Evaluacion.EF, CalcGrade("2007202389", "exfinl.txt"))
                    End With
                    lst.Add(std)
                Catch ex As Microsoft.VisualBasic.
                            FileIO.MalformedLineException
                    Console.WriteLine("Line " & ex.Message &
                    "Is Not valid And will be skipped.")
                End Try
            End While
        End Using

        'For Each preg In pregList
        '    Console.WriteLine(preg01.Name & "" & preg01.Score)
        'Next

        'For Each s As Student In lst
        '    Console.WriteLine("{0} {1}", CStr(s.Id), s.Name)
        'Next
        Return lst

    End Function

    Public Shared Function CalcGrade(course As Course, eval As Evaluation) As Double

        Dim grade As Double = 0

        Select Case course

            Case Course.sim162 ' TODO CalcGrade sim162

                Console.WriteLine("Ingrese sus respuestas... ")
                Console.WriteLine()

                'Dim pdata = GetQuestionData("prac01.txt")
                Dim pdata = GetQuestionData(eval)

                Do
                    grade = 0
                    For Each preg In pdata
                        Console.Write(" Respuesta " & preg.Name & ": ")
                        Dim ansStr = Console.ReadLine()
                        Dim ans = Convert.ToInt32(ansStr, 2)
                        grade += IIf(ans = preg.Answer, preg.Score, 0)
                        Console.Write(ControlChars.Cr)
                    Next
                    Console.Write("CONFIRMAR EVALUACION (SI/NO): ")
                Loop While (Console.ReadLine().Trim().ToUpper() = "NO")


            Case Course.pweb162 ' TODO CalcGrade pweb162
            Case Course.so162 ' TODO CalcGrade so162
            Case Course.visual162 ' TODO CalcGrade visual162
        End Select

        Return grade

    End Function

    Private Shared Function CalcGrade(id As String, fileName As String) As Double

        Dim grade As Double = 0
        Dim path As String = IO.Path.Combine(My.Application.Info.DirectoryPath, fileName)

        Using MyReader As New Microsoft.VisualBasic.FileIO.TextFieldParser(path)

            MyReader.TextFieldType = FileIO.FieldType.Delimited
            MyReader.SetDelimiters(vbTab)

            Dim currentRow As String()

            While Not MyReader.EndOfData
                Try
                    currentRow = MyReader.ReadFields()

                    If id = currentRow.ElementAt(0) Then

                        Dim pdata = GetQuestionData(fileName)
                        For Each preg In pdata
                            Dim stdAnswer = Convert.ToInt32(currentRow.ElementAt(preg.Num), 2)
                            grade += IIf(stdAnswer = preg.Answer, preg.Score, 0)
                        Next

                    End If

                Catch ex As Microsoft.VisualBasic.FileIO.MalformedLineException
                    Console.WriteLine("Line " & ex.Message & "Is Not valid And will be skipped.")
                End Try
            End While
        End Using
        Console.WriteLine("Id: " & id & " Grade: " & grade)
        Return grade
    End Function

    Public Shared Function GetQuestionData(evaluation As Evaluation) As ArrayList

        Dim pdata As ArrayList = Nothing

        Select Case evaluation

            Case Evaluation.EP
                ' TODO Evaluation.P1
                ' TODO Evaluation.P1 schema
                ' TODO Evaluation.P1 modificaciones preguntas compuestas
                Dim p1 = New With {Key .Name = "P1", .Answer = Alternativa.B, .Score = 0.5, .Num = 1}
                Dim p2_1 = New With {Key .Name = "P2.1", .Answer = Alternativa.C, .Score = 0.25, .Num = 2}
                Dim p2_2 = New With {Key .Name = "P2.2", .Answer = Alternativa.Proposito, .Score = 0.25, .Num = 3}
                Dim p3_1 = New With {Key .Name = "P3.1", .Answer = Alternativa.D, .Score = 0.25, .Num = 4}
                Dim p3_2 = New With {Key .Name = "P3.2", .Answer = Alternativa.Ventaja, .Score = 0.25, .Num = 5}
                Dim p4_1 = New With {Key .Name = "P4.1", .Answer = Alternativa.D, .Score = 0.25, .Num = 6}
                Dim p4_2 = New With {Key .Name = "P4.2", .Answer = Alternativa.Desventaja, .Score = 0.25, .Num = 7}
                Dim p5_1 = New With {Key .Name = "P5.1", .Answer = Alternativa.NoAnswer, .Score = 0.25, .Num = 8}
                Dim p5_2 = New With {Key .Name = "P5.2", .Answer = Alternativa.NoAnswer, .Score = 0.25, .Num = 9}
                Dim p6_1 = New With {Key .Name = "P6.1", .Answer = Alternativa.C, .Score = 0.25, .Num = 10}
                Dim p6_2 = New With {Key .Name = "P6.2", .Answer = Alternativa.OtrasCons, .Score = 0.25, .Num = 11}
                Dim p7_1 = New With {Key .Name = "P7.1", .Answer = Alternativa.D, .Score = 0.25, .Num = 12}
                Dim p7_2 = New With {Key .Name = "P7.2", .Answer = Alternativa.Proposito, .Score = 0.25, .Num = 13}
                Dim p8 = New With {Key .Name = "P8", .Answer = Alternativa.A, .Score = 0.5, .Num = 14}
                Dim p9 = New With {Key .Name = "P9", .Answer = Alternativa.C, .Score = 0.5, .Num = 15}
                Dim p10_1 = New With {Key .Name = "P10.1", .Answer = Alternativa.B, .Score = 0.5, .Num = 16}
                Dim p10_2 = New With {Key .Name = "P10.2", .Answer = Alternativa.Entidad, .Score = 0.5, .Num = 17}
                Dim p11_1 = New With {Key .Name = "P11.1", .Answer = Alternativa.NoAnswer, .Score = 0.5, .Num = 18}
                Dim p11_2 = New With {Key .Name = "P11.2", .Answer = Alternativa.NoAnswer, .Score = 0.5, .Num = 19}
                Dim p12_1 = New With {Key .Name = "P12.1", .Answer = Alternativa.A, .Score = 0.5, .Num = 20}
                Dim p12_2 = New With {Key .Name = "P12.2", .Answer = Alternativa.Recurso, .Score = 0.5, .Num = 21}
                Dim p13_1 = New With {Key .Name = "P13.1", .Answer = Alternativa.C, .Score = 0.5, .Num = 22}
                Dim p13_2 = New With {Key .Name = "P13.2", .Answer = Alternativa.Recurso, .Score = 0.5, .Num = 23}
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
            Case Evaluation.EF
                Dim p1 = New With {Key .Name = "P1", .Answer = Alternativa.B, .Score = 0.2, .Num = 1}

                Dim p2_1 = New With {Key .Name = "P2.1", .Answer = Alternativa.C, .Score = 0.1, .Num = 2}
                Dim p2_2 = New With {Key .Name = "P2.2", .Answer = Alternativa.Proposito, .Score = 0.1, .Num = 3}

                Dim p3_1 = New With {Key .Name = "P3.1", .Answer = Alternativa.D, .Score = 0.1, .Num = 4}
                Dim p3_2 = New With {Key .Name = "P3.2", .Answer = Alternativa.Ventaja, .Score = 0.1, .Num = 5}

                Dim p4_1 = New With {Key .Name = "P4.1", .Answer = Alternativa.D, .Score = 0.1, .Num = 6}
                Dim p4_2 = New With {Key .Name = "P4.2", .Answer = Alternativa.Desventaja, .Score = 0.1, .Num = 7}

                Dim p5_1 = New With {Key .Name = "P5.1", .Answer = Alternativa.NoAnswer, .Score = 0.1, .Num = 8}
                Dim p5_2 = New With {Key .Name = "P5.2", .Answer = Alternativa.NoAnswer, .Score = 0.1, .Num = 9}

                Dim p6_1 = New With {Key .Name = "P6.1", .Answer = Alternativa.C, .Score = 0.1, .Num = 10}
                Dim p6_2 = New With {Key .Name = "P6.2", .Answer = Alternativa.OtrasCons, .Score = 0.1, .Num = 11}

                Dim p7_1 = New With {Key .Name = "P7.1", .Answer = Alternativa.D, .Score = 0.1, .Num = 12}
                Dim p7_2 = New With {Key .Name = "P7.2", .Answer = Alternativa.Proposito, .Score = 0.1, .Num = 13}

                Dim p8 = New With {Key .Name = "P8", .Answer = Alternativa.A, .Score = 0.2, .Num = 14}
                Dim p9 = New With {Key .Name = "P9", .Answer = Alternativa.C, .Score = 0.2, .Num = 15}

                Dim p10_1 = New With {Key .Name = "P10.1", .Answer = Alternativa.B, .Score = 0.1, .Num = 16}
                Dim p10_2 = New With {Key .Name = "P10.2", .Answer = Alternativa.Entidad, .Score = 0.1, .Num = 17}

                Dim p11_1 = New With {Key .Name = "P11.1", .Answer = Alternativa.NoAnswer, .Score = 0.1, .Num = 18}
                Dim p11_2 = New With {Key .Name = "P11.2", .Answer = Alternativa.NoAnswer, .Score = 0.1, .Num = 19}

                Dim p12_1 = New With {Key .Name = "P12.1", .Answer = Alternativa.A, .Score = 0.1, .Num = 20}
                Dim p12_2 = New With {Key .Name = "P12.2", .Answer = Alternativa.Recurso, .Score = 0.1, .Num = 21}

                Dim p13_1 = New With {Key .Name = "P13.1", .Answer = Alternativa.C, .Score = 0.1, .Num = 22}
                Dim p13_2 = New With {Key .Name = "P13.2", .Answer = Alternativa.Recurso, .Score = 0.1, .Num = 23}

                Dim p14 = New With {Key .Name = "P14", .Answer = Alternativa.A Or Alternativa.C, .Score = 0.2, .Num = 24}

                Dim p15 = New With {Key .Name = "P15", .Answer = Alternativa.A, .Score = 0.3, .Num = 25}
                Dim p16 = New With {Key .Name = "P16", .Answer = Alternativa.D, .Score = 0.3, .Num = 26}
                Dim p17 = New With {Key .Name = "P17", .Answer = Alternativa.C, .Score = 0.3, .Num = 27}
                Dim p18 = New With {Key .Name = "P18", .Answer = Alternativa.B, .Score = 0.3, .Num = 28}
                Dim p19 = New With {Key .Name = "P19", .Answer = Alternativa.NoAnswer, .Score = 0.2, .Num = 29}
                Dim p20 = New With {Key .Name = "P20", .Answer = Alternativa.C Or Alternativa.D, .Score = 0.3, .Num = 30}
                Dim p21 = New With {Key .Name = "P21", .Answer = Alternativa.A Or Alternativa.B Or Alternativa.C Or Alternativa.D, .Score = 0.3, .Num = 31}
                Dim p22 = New With {Key .Name = "P22", .Answer = Alternativa.A Or Alternativa.B Or Alternativa.C Or Alternativa.D, .Score = 0.3, .Num = 32}
                Dim p23 = New With {Key .Name = "P23", .Answer = Alternativa.C, .Score = 0.3, .Num = 33}
                Dim p24 = New With {Key .Name = "P24", .Answer = Alternativa.B, .Score = 0.3, .Num = 34}
                Dim p25 = New With {Key .Name = "P25", .Answer = Alternativa.A, .Score = 0.3, .Num = 35}
                Dim p26 = New With {Key .Name = "P26", .Answer = Alternativa.D, .Score = 0.3, .Num = 36}

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


    Public Shared Function GetQuestionData(fileName As String) As ArrayList

        Dim pdata As ArrayList = Nothing

        Select Case fileName

            Case "prac01.txt"
                ' TODO prac01.txt
                ' TODO prac01.txt schema
                ' TODO prac01.txt modificaciones preguntas compuestas
                Dim p1 = New With {Key .Name = "P1", .Answer = Alternativa.B, .Score = 0.5, .Num = 1}
                Dim p2_1 = New With {Key .Name = "P2.1", .Answer = Alternativa.C, .Score = 0.25, .Num = 2}
                Dim p2_2 = New With {Key .Name = "P2.2", .Answer = Alternativa.Proposito, .Score = 0.25, .Num = 3}
                Dim p3_1 = New With {Key .Name = "P3.1", .Answer = Alternativa.D, .Score = 0.25, .Num = 4}
                Dim p3_2 = New With {Key .Name = "P3.2", .Answer = Alternativa.Ventaja, .Score = 0.25, .Num = 5}
                Dim p4_1 = New With {Key .Name = "P4.1", .Answer = Alternativa.D, .Score = 0.25, .Num = 6}
                Dim p4_2 = New With {Key .Name = "P4.2", .Answer = Alternativa.Desventaja, .Score = 0.25, .Num = 7}
                Dim p5_1 = New With {Key .Name = "P5.1", .Answer = Alternativa.NoAnswer, .Score = 0.25, .Num = 8}
                Dim p5_2 = New With {Key .Name = "P5.2", .Answer = Alternativa.NoAnswer, .Score = 0.25, .Num = 9}
                Dim p6_1 = New With {Key .Name = "P6.1", .Answer = Alternativa.C, .Score = 0.25, .Num = 10}
                Dim p6_2 = New With {Key .Name = "P6.2", .Answer = Alternativa.OtrasCons, .Score = 0.25, .Num = 11}
                Dim p7_1 = New With {Key .Name = "P7.1", .Answer = Alternativa.D, .Score = 0.25, .Num = 12}
                Dim p7_2 = New With {Key .Name = "P7.2", .Answer = Alternativa.Proposito, .Score = 0.25, .Num = 13}
                Dim p8 = New With {Key .Name = "P8", .Answer = Alternativa.A, .Score = 0.5, .Num = 14}
                Dim p9 = New With {Key .Name = "P9", .Answer = Alternativa.C, .Score = 0.5, .Num = 15}
                Dim p10_1 = New With {Key .Name = "P10.1", .Answer = Alternativa.B, .Score = 0.5, .Num = 16}
                Dim p10_2 = New With {Key .Name = "P10.2", .Answer = Alternativa.Entidad, .Score = 0.5, .Num = 17}
                Dim p11_1 = New With {Key .Name = "P11.1", .Answer = Alternativa.NoAnswer, .Score = 0.5, .Num = 18}
                Dim p11_2 = New With {Key .Name = "P11.2", .Answer = Alternativa.NoAnswer, .Score = 0.5, .Num = 19}
                Dim p12_1 = New With {Key .Name = "P12.1", .Answer = Alternativa.A, .Score = 0.5, .Num = 20}
                Dim p12_2 = New With {Key .Name = "P12.2", .Answer = Alternativa.Recurso, .Score = 0.5, .Num = 21}
                Dim p13_1 = New With {Key .Name = "P13.1", .Answer = Alternativa.C, .Score = 0.5, .Num = 22}
                Dim p13_2 = New With {Key .Name = "P13.2", .Answer = Alternativa.Recurso, .Score = 0.5, .Num = 23}
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

            Case "exprcl.txt"
                ' TODO exprcl.txt
                ' TODO exprcl.txt schema
            Case "prac02.txt"
                ' TODO prac02.txt
                ' TODO prac02.txt schema
            Case "exfinl.txt"
                ' TODO exfinl.txt
                ' TODO exfinl.txt schema
            Case Else
                Throw New Exception("Wrong File Name " & fileName & " :'(")
        End Select

        Return pdata

    End Function
End Class

