
Imports System.IO
Imports System.Text
Imports System.Text.RegularExpressions
Imports Newtonsoft.Json


Public Class So162M

#Region " GetStudents "

    Public Shared Function GetStudents() As List(Of Student)

        Dim lst As List(Of Student) = Nothing
        Dim filepath As String = Path.Combine(My.Application.Info.DirectoryPath, "so162-lst-json.txt")
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

#End Region



    Public Shared Sub InitGradeMngr()

        Console.WriteLine(" -------------------------------")
        Console.WriteLine("| SISTEMAS OPERATIVOS 2016-2    |")
        Console.WriteLine("| EVALUATION GRADES MANAGER     |")
        Console.WriteLine(" -------------------------------")
        Console.WriteLine()

        Dim serializer As New JsonSerializer() With {.Formatting = Formatting.Indented}
        Dim path As String = IO.Path.Combine(My.Application.Info.DirectoryPath, "so162-lst-json.txt")
        Dim stdlst = So162M.GetStudents()

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
            Console.WriteLine(" [6] EVALUAR / INGRESAR NOTA EF")
            Console.WriteLine()
            Console.Write("INGRESA UNA OPCION: ")
            optn = Console.ReadLine()
        Loop Until (isValidOption(optn))


        Select Case optn

            Case "1" ' MOSTRAR LISTA

                Dim sb As New StringBuilder()
                sb.AppendLine()
                sb.AppendFormat("  {0,-10} {1, -40} {2}", "Id", "Name", vbCrLf)

                For Each std As Student In stdlst
                    sb.AppendFormat("  {0,-10} {1, -40} {2}", std.Id, std.Name, vbCrLf)
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
                        Console.Write("INGRESAR NOTAS A PARTIR DEL INDICE: ")
                        Integer.TryParse(Console.ReadLine(), ind)
                    Case "3"
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
                            Using sw As New StreamWriter(path), writer As New JsonTextWriter(sw)
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
                            Using sw As New StreamWriter(path), writer As New JsonTextWriter(sw)
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
                            Using sw As New StreamWriter(path), writer As New JsonTextWriter(sw)
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
                            Using sw As New StreamWriter(path), writer As New JsonTextWriter(sw)
                                serializer.Serialize(writer, stdlst)
                            End Using
                        Next
                End Select

            Case "6"

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
                        Console.Write("INGRESAR NOTAS A PARTIR DEL INDICE: ")
                        Integer.TryParse(Console.ReadLine(), ind)
                    Case "3"
                        Console.WriteLine()
                        Console.Write("INGRESAR NOTAS DE ALUMNO CON NUMERO DE ORDEN: ")
                        Integer.TryParse(Console.ReadLine(), ind)
                End Select

                Console.WriteLine()


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

                    Dim mdcpath = My.Computer.FileSystem.SpecialDirectories.MyDocuments
                    Dim crspath = IO.Path.Combine(mdcpath, "So162")
                    Dim stdpath = IO.Path.Combine(crspath, std.Name)
                    Dim evlpath = IO.Path.Combine(stdpath, "E2-EXFNL")
                    Dim jflpath = IO.Path.Combine(evlpath, "so162-exfinal-json.txt")

                    Console.WriteLine()
                    Console.WriteLine(std.Name)
                    Console.WriteLine(Directory.Exists(evlpath))
                    Console.WriteLine(File.Exists(jflpath))

                    'Console.WriteLine(" " & std.Name)
                    'Console.Write(" NOTA EP: ")
                    'std.Grades.Item(Evaluation.EP) = Double.Parse(Console.ReadLine())
                    'Console.WriteLine()
                    'Using sw As New StreamWriter(path), writer As New JsonTextWriter(sw)
                    '    serializer.Serialize(writer, stdlst)
                    'End Using
                Next


        End Select

        Console.WriteLine()

    End Sub

    Public Shared Sub InitEvalFilesMngr()

        Console.WriteLine(" -------------------------------")
        Console.WriteLine("| SISTEMAS OPERATIVOS 2016-2    |")
        Console.WriteLine("| EVALUATION FILES MANAGER      |")
        Console.WriteLine(" -------------------------------")
        Console.WriteLine()

        Dim serializer As New JsonSerializer() With {.Formatting = Formatting.Indented}
        Dim path As String = IO.Path.Combine(My.Application.Info.DirectoryPath, "so162-lst-json.txt")
        Dim stdlst = So162M.GetStudents()

        Dim optn As String

        Dim isValidOption = Function(x)
                                If Not Regex.IsMatch(x, "^[123]{1,1}$") Then
                                    Console.WriteLine("OPCION NO VALIDA, INTENTALO DE NUEVO.")
                                    Console.WriteLine()
                                    Return False
                                End If
                                Return True
                            End Function

        Do
            Console.WriteLine("MENU PRINCIPAL")
            Console.WriteLine()
            Console.WriteLine("  [1] MOSTRAR LISTA DE ESTUDIANTES")
            Console.WriteLine("  [2] DESCARGAR ARCHIVOS DE EVALUACIONES")
            Console.WriteLine("  [3] GENERAR DIRECTORIOS DE EVALUACIONES")
            Console.WriteLine()
            Console.Write("INGRESA UNA OPCION: ")
            optn = Console.ReadLine()
        Loop Until (isValidOption(optn))

        Select Case optn

            Case "1" ' MOSTRAR LISTA DE ESTUDIANTES
            Case "2" ' DESCARGAR ARCHIVOS DE EVALUACION

                Dim mdcpath = My.Computer.FileSystem.SpecialDirectories.MyDocuments
                Dim crspath = IO.Path.Combine(mdcpath, "So162")

                For Each std As Student In stdlst

                    Console.WriteLine()
                    Console.WriteLine(std.Name)
                    Console.Write("DESCARGAR EVALUACIONES (SI/NO): ")

                    If Console.ReadLine().Trim.Equals("NO", StringComparison.OrdinalIgnoreCase) Then
                        Continue For
                    End If

                    Diagnostics.Process.Start(IO.Path.Combine(crspath, std.Name))

                Next

            Case "3" ' GENERAR DIRECTORIOS DE EVALUACIONES

                GenerateStudentDirectories()
                Console.WriteLine()
                Console.WriteLine(" ¡DIRECTORIOS CREADOS SATISFACTORIAMENTE!")
                Console.WriteLine()
        End Select

    End Sub

    Public Shared Sub GenerateStudentDirectories()

        Dim lst = So162M.GetStudents()
        Dim mdcpath = My.Computer.FileSystem.SpecialDirectories.MyDocuments
        Dim crspath = Path.Combine(mdcpath, "So162")
        Directory.CreateDirectory(crspath)

        Dim stpath As String

        For Each std As Student In lst
            stpath = Path.Combine(crspath, std.Name)
            Directory.CreateDirectory(stpath)
            Directory.CreateDirectory(Path.Combine(stpath, "E1-EXPCL"))
            Directory.CreateDirectory(Path.Combine(stpath, "E2-EXFNL"))
        Next

    End Sub




End Class
