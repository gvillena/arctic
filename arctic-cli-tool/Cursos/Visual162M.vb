Imports System.IO
Imports System.Text
Imports System.Text.RegularExpressions
Imports LibGit2Sharp
Imports Newtonsoft.Json

Public Class Visual162M

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Public Shared Function GetStudents() As List(Of Student)

        Dim lst As List(Of Student) = Nothing
        Dim filepath As String = Path.Combine(My.Application.Info.DirectoryPath, "visual162-lst-json.txt")
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

    ''' <summary>
    ''' 
    ''' </summary>
    Public Shared Sub InitGradeMngr()

        Console.WriteLine("-------------------")
        Console.WriteLine("-- VISUAL 2016-2 --")
        Console.WriteLine("-------------------")
        Console.WriteLine()

        Dim serializer As New JsonSerializer() With {.Formatting = Formatting.Indented}
        Dim stdpath As String = IO.Path.Combine(My.Application.Info.DirectoryPath, "visual162-lst-json.txt")
        Dim stdlst = Visual162M.GetStudents()

        Dim optn As String

        Dim isValidOption = Function(x)
                                If Not Regex.IsMatch(x, "^[123456]{1,1}$") Then
                                    Console.WriteLine("OPCION NO VALIDA, INTENTALO DE NUEVO.")
                                    Console.WriteLine()
                                    Return False
                                End If
                                Return True
                            End Function

        Do
            Console.WriteLine("MENU PRINCIPAL")
            Console.WriteLine()
            Console.WriteLine(" [1] MOSTRAR LISTA")
            Console.WriteLine(" [2] MOSTRAR LISTA + NOTAS")
            Console.WriteLine(" [3] MOSTRAR LISTA + PROMEDIO")
            Console.WriteLine(" [4] MOSTRAR LISTA + NOTAS + PROMEDIO")
            Console.WriteLine(" [5] INGRESAR NOTAS")
            Console.WriteLine(" [6] DESCARGAR EVALUACION")
            Console.WriteLine()
            Console.Write("INGRESA UNA OPCION: ")
            optn = Console.ReadLine()
        Loop Until (isValidOption(optn))


        Select Case optn

            Case "1" ' MOSTRAR LISTA

                Dim sb As New StringBuilder()
                sb.AppendLine()
                sb.AppendFormat("  {0,-2} {1,-10} {2, -40} {3}", "N", "Id", "Name", vbCrLf)
                For Each std As Student In stdlst
                    sb.AppendFormat("  {0,2} {1,-10} {2, -40} {3}", String.Format("{0:00}", stdlst.IndexOf(std) + 1), std.Id, std.Name, vbCrLf)
                Next
                Console.WriteLine(sb.ToString)

            Case "2" ' MOSTRAR LISTA + NOTAS

                Dim inputEval As String
                Dim isValidEval = Function(x)
                                      If Not Regex.IsMatch(x, "^[1234]{1,1}$") Then
                                          Console.WriteLine("OPCION NO VALIDA, INTENTALO DE NUEVO.")
                                          Console.WriteLine()
                                          Return False
                                      End If
                                      Return True
                                  End Function
                Do
                    Console.WriteLine()
                    Console.WriteLine("EVALUACIONES")
                    Console.WriteLine()
                    Console.WriteLine(" [1] PRACTICA 01")
                    Console.WriteLine(" [2] EXAMEN PARCIAL")
                    Console.WriteLine(" [3] PRACTICA 02")
                    Console.WriteLine(" [4] EXAMEN FINAL")
                    Console.WriteLine()
                    Console.Write("INGRESA UNA OPCION: ")
                    inputEval = Console.ReadLine()
                Loop Until isValidEval(optn)

                Select Case inputEval

                    Case "1"
                        Dim sb As New StringBuilder()
                        sb.AppendLine()
                        sb.AppendFormat(" {0, -38} {1, -4} {2}", "Name", "P1", vbCrLf)
                        sb.AppendFormat(" {0, -38} {1, -4} {2}", "----", "--", vbCrLf)
                        For Each std As Student In stdlst
                            sb.AppendFormat(" {0, -38} {1, -4} {2}",
                                                std.Name,
                                                String.Format("{0:#0.00}", std.Grades(Evaluation.P1)),
                                                vbCrLf)
                        Next
                        Console.WriteLine(sb.ToString)
                    Case "2"
                        Dim sb As New StringBuilder()
                        sb.AppendLine()
                        sb.AppendFormat(" {0, -38} {1, -4} {2}", "Name", "EP", vbCrLf)
                        sb.AppendFormat(" {0, -38} {1, -4} {2}", "----", "--", vbCrLf)
                        For Each std As Student In stdlst
                            sb.AppendFormat(" {0, -38} {1, -4} {2}",
                                                std.Name,
                                                String.Format("{0:#0.00}", std.Grades(Evaluation.EP)),
                                                vbCrLf)
                        Next
                        Console.WriteLine(sb.ToString)
                    Case "3"
                        Dim sb As New StringBuilder()
                        sb.AppendLine()
                        sb.AppendFormat(" {0, -38} {1, -4} {2}", "Name", "P2", vbCrLf)
                        sb.AppendFormat(" {0, -38} {1, -4} {2}", "----", "--", vbCrLf)
                        For Each std As Student In stdlst
                            sb.AppendFormat(" {0, -38} {1, -4} {2}",
                                                std.Name,
                                                String.Format("{0:#0.00}", std.Grades(Evaluation.P2)),
                                                vbCrLf)
                        Next
                        Console.WriteLine(sb.ToString)
                    Case "4"
                        Dim sb As New StringBuilder()
                        sb.AppendLine()
                        sb.AppendFormat(" {0, -38} {1, -4} {2}", "Name", "EF", vbCrLf)
                        sb.AppendFormat(" {0, -38} {1, -4} {2}", "----", "--", vbCrLf)
                        For Each std As Student In stdlst
                            sb.AppendFormat(" {0, -38} {1, -4} {2}",
                                                std.Name,
                                                String.Format("{0:#0.00}", std.Grades(Evaluation.EF)),
                                                vbCrLf)
                        Next
                        Console.WriteLine(sb.ToString)
                End Select

            Case "3"
                Dim sb As New StringBuilder()
                sb.AppendLine()
                sb.AppendFormat(" {0, -38} {1, -4} {2}", "Name", "PROM", vbCrLf)
                sb.AppendFormat(" {0, -38} {1, -4} {2}", "----", "----", vbCrLf)
                For Each std As Student In stdlst
                    sb.AppendFormat(" {0, -38} {1, -4} {2}",
                                                std.Name,
                                                String.Format("{0:#0.00}", std.FinalGrade),
                                                vbCrLf)
                Next
                Console.WriteLine(sb.ToString)

            Case "4" ' MOSTRAR LISTA + NOTAS + PROMEDIO

                Dim sb As New StringBuilder()
                sb.AppendLine()
                sb.AppendFormat(" {0, -38}  {1, -4}  {2, -4}  {3, -4}  {4, -4}  {5, -4} {6}",
                                  "Name", "P1", "EP", "P2", "EF", "PROM", vbCrLf)
                For Each std As Student In stdlst
                    sb.AppendFormat(" {0, -38}  {1, -4}  {2, -4}  {3, -4}  {4, -4}  {5, -4} {6}",
                                      std.Name,
                                      String.Format("{0:#0.00}", std.Grades(Evaluation.P1)),
                                      String.Format("{0:#0.00}", std.Grades(Evaluation.EP)),
                                      String.Format("{0:#0.00}", std.Grades(Evaluation.P2)),
                                      String.Format("{0:#0.00}", std.Grades(Evaluation.EF)),
                                      String.Format("{0:#0.00}", std.FinalGrade),
                                      vbCrLf)
                Next
                Console.WriteLine(sb.ToString)

            Case "5" ' INGRESAR NOTAS 

                Dim inputEval As String
                Dim isValidEval = Function(x)
                                      If Not Regex.IsMatch(x, "^[1234]{1,1}$") Then
                                          Console.WriteLine("OPCION NO VALIDA, INTENTALO DE NUEVO.")
                                          Console.WriteLine()
                                          Return False
                                      End If
                                      Return True
                                  End Function
                Do
                    Console.WriteLine()
                    Console.WriteLine("EVALUACIONES")
                    Console.WriteLine()
                    Console.WriteLine(" [1] PRACTICA 01")
                    Console.WriteLine(" [2] EXAMEN PARCIAL")
                    Console.WriteLine(" [3] PRACTICA 02")
                    Console.WriteLine(" [4] EXAMEN FINAL")
                    Console.WriteLine()
                    Console.Write("INGRESA UNA OPCION: ")
                    inputEval = Console.ReadLine()
                Loop Until isValidEval(inputEval)

                Console.WriteLine()

                Dim inputMode As String
                Dim isValidMode = Function(x)
                                      If Not Regex.IsMatch(x, "^[123]{1,1}$") Then
                                          Console.WriteLine("OPCION NO VALIDA, INTENTALO DE NUEVO.")
                                          Console.WriteLine()
                                          Return False
                                      End If
                                      Return True
                                  End Function

                Do
                    Console.WriteLine("MODO DE INGRESO")
                    Console.WriteLine()
                    Console.WriteLine(" [1] TODAS LAS NOTAS POR LISTA DESDE EL PRINCIPIO.")
                    Console.WriteLine(" [2] TODAS LAS NOTAS POR LISTA A PARTIR DEL INDICE INGRESADO.")
                    Console.WriteLine(" [3] SOLO UNA NOTA SEGUN NUMERO DE ORDEN INGRESADO.")
                    Console.WriteLine()
                    Console.Write("INGRESA UNA OPCION: ")
                    inputMode = Console.ReadLine()
                Loop Until (isValidOption(inputMode))

                Dim ind As Integer = 0

                Select Case inputMode
                    Case "1"
                        ' Nothing
                    Case "2"
                        Console.WriteLine()
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
                        Console.Write("INGRESAR NOTAS A PARTIR DEL INDICE: ")
                        Integer.TryParse(Console.ReadLine(), ind)
                    Case "3"

                        Console.WriteLine()
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
                        Console.Write("INGRESAR NOTAS DE ALUMNO CON NUMERO DE ORDEN: ")
                        Integer.TryParse(Console.ReadLine(), ind)
                End Select

                Console.WriteLine()

                Select Case inputEval

                    Case "1"

                        For Each std As Student In stdlst

                            Select Case inputMode
                                Case "1"
                                    ' Nothing
                                Case "2"
                                    If stdlst.IndexOf(std) < ind - 1 Then
                                        Continue For
                                    End If
                                Case "3"
                                    If stdlst.IndexOf(std) <> ind - 1 Then
                                        Continue For
                                    End If
                            End Select

                            Console.WriteLine(" " & std.Name)
                            Console.Write(" NOTA P1: ")
                            std.Grades.Item(Evaluation.P1) = Double.Parse(Console.ReadLine())
                            Console.WriteLine()
                            Using sw As New StreamWriter(stdpath), writer As New JsonTextWriter(sw)
                                serializer.Serialize(writer, stdlst)
                            End Using
                        Next

                    Case "2"


                        For Each std As Student In stdlst

                            Select Case inputMode
                                Case "1"
                                    ' Nothing
                                Case "2"
                                    If stdlst.IndexOf(std) < ind - 1 Then
                                        Continue For
                                    End If
                                Case "3"
                                    If stdlst.IndexOf(std) <> ind - 1 Then
                                        Continue For
                                    End If
                            End Select

                            Console.WriteLine(" " & std.Name)
                            Console.Write(" NOTA EP: ")
                            std.Grades.Item(Evaluation.EP) = Double.Parse(Console.ReadLine())
                            Console.WriteLine()
                            Using sw As New StreamWriter(stdpath), writer As New JsonTextWriter(sw)
                                serializer.Serialize(writer, stdlst)
                            End Using
                        Next

                    Case "3"


                        For Each std As Student In stdlst

                            Select Case inputMode
                                Case "1"
                                    ' Nothing
                                Case "2"
                                    If stdlst.IndexOf(std) < ind - 1 Then
                                        Continue For
                                    End If
                                Case "3"
                                    If stdlst.IndexOf(std) <> ind - 1 Then
                                        Continue For
                                    End If
                            End Select

                            Console.WriteLine(" " & std.Name)
                            Console.Write(" NOTA P2: ")
                            std.Grades.Item(Evaluation.P2) = Double.Parse(Console.ReadLine())
                            Console.WriteLine()
                            Using sw As New StreamWriter(stdpath), writer As New JsonTextWriter(sw)
                                serializer.Serialize(writer, stdlst)
                            End Using
                        Next

                    Case "4"


                        For Each std As Student In stdlst

                            Select Case inputMode
                                Case "1"
                                    ' Nothing
                                Case "2"
                                    If stdlst.IndexOf(std) < ind - 1 Then
                                        Continue For
                                    End If
                                Case "3"
                                    If stdlst.IndexOf(std) <> ind - 1 Then
                                        Continue For
                                    End If
                            End Select

                            Console.WriteLine(" " & std.Name)
                            Console.Write(" NOTA EF: ")
                            std.Grades.Item(Evaluation.EF) = Double.Parse(Console.ReadLine())
                            Console.WriteLine()
                            Using sw As New StreamWriter(stdpath), writer As New JsonTextWriter(sw)
                                serializer.Serialize(writer, stdlst)
                            End Using
                        Next
                End Select

            Case "6"

                Console.WriteLine()
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

                Dim ind As Integer
                Console.Write("NUMERO DE INDICE DE ALUMNO: ")
                Integer.TryParse(Console.ReadLine(), ind)

                Dim inputEval As String
                Dim isValidEval = Function(x)
                                      If Not Regex.IsMatch(x, "^[1234]{1,1}$") Then
                                          Console.WriteLine("OPCION NO VALIDA, INTENTALO DE NUEVO.")
                                          Console.WriteLine()
                                          Return False
                                      End If
                                      Return True
                                  End Function
                Do
                    Console.WriteLine()
                    Console.WriteLine("EVALUACIONES")
                    Console.WriteLine()
                    Console.WriteLine(" [1] PRACTICA 01")
                    Console.WriteLine(" [2] EXAMEN PARCIAL")
                    Console.WriteLine(" [3] PRACTICA 02")
                    Console.WriteLine(" [4] EXAMEN FINAL")
                    Console.WriteLine()
                    Console.Write("INGRESA UNA OPCION: ")
                    inputEval = Console.ReadLine()
                Loop Until isValidEval(inputEval)

                Dim rurl As String
                Console.Write("GITHUB URL: ")
                rurl = Console.ReadLine()

                Dim rname As String = IO.Path.GetFileNameWithoutExtension(rurl.Replace("https://", ""))
                Dim evalPath As New Dictionary(Of Integer, String) From {{1, "Practica 01"}, {2, "Examen Parcial"}, {3, "Practica 02"}, {4, "Examen Final"}}
                Dim rpth As String = IO.Path.Combine(evalPath(Integer.Parse(inputEval)), rname)
                Directory.CreateDirectory(rpth)
                Repository.Clone(rurl, rpth)
                Diagnostics.Process.Start(rpth)
                Console.WriteLine()

        End Select

        Console.WriteLine()

    End Sub

    Public Shared Sub GenerateStudentDirectories()

        Dim lst = Visual162M.GetStudents()
        Dim mdcpath = My.Computer.FileSystem.SpecialDirectories.MyDocuments
        Dim crspath = Path.Combine(mdcpath, "Visual162")
        Directory.CreateDirectory(crspath)

        Dim stpath As String

        For Each std As Student In lst
            stpath = Path.Combine(crspath, std.Name)
            Directory.CreateDirectory(stpath)
            Directory.CreateDirectory(Path.Combine(stpath, "Practica 01"))
            Directory.CreateDirectory(Path.Combine(stpath, "Examen Parcial"))
            Directory.CreateDirectory(Path.Combine(stpath, "Practica 02"))
            Directory.CreateDirectory(Path.Combine(stpath, "Examen Final"))
        Next

    End Sub

End Class
