Imports System.IO
Imports System.Text
Imports System.Text.RegularExpressions
Imports LibGit2Sharp
Imports Newtonsoft.Json

''' <summary>
''' Pweb162
''' </summary>
Public Class Pweb162M


    Public Shared Function GetStudents() As List(Of Student)

        Dim lst As List(Of Student) = Nothing
        Dim filepath As String = Path.Combine(My.Application.Info.DirectoryPath, "pweb162-lst-json.txt")
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

        Console.WriteLine("-------------------------")
        Console.WriteLine("- PLATAFORMA WEB 2016-2 -")
        Console.WriteLine("-------------------------")
        Console.WriteLine()

        Dim serializer As New JsonSerializer() With {.Formatting = Formatting.Indented}
        Dim path As String = IO.Path.Combine(My.Application.Info.DirectoryPath, "pweb162-lst-json.txt")
        Dim stdlst = Pweb162M.GetStudents()

        Dim optn As String

        Dim isValidOption = Function(x)
                                If Not Regex.IsMatch(x, "^[12345]{1,1}$") Then
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


        End Select

        Console.WriteLine()

    End Sub

    Public Shared Sub InitEvalFilesMngr()

        Console.WriteLine("----------------------------")
        Console.WriteLine("| PLATAFORMA WEB 2016-2    |")
        Console.WriteLine("| EVALUATION FILES MANAGER |")
        Console.WriteLine("----------------------------")
        Console.WriteLine()

        Dim serializer As New JsonSerializer() With {.Formatting = Formatting.Indented}
        Dim path As String = IO.Path.Combine(My.Application.Info.DirectoryPath, "pweb162-lst-json.txt")
        Dim stdlst = Pweb162M.GetStudents()

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
                Dim sb As New StringBuilder()
                sb.AppendLine()
                sb.AppendFormat(" {0,-2} {1,-10} {2, -40} {3}", "N", "ID", "NAME", vbCrLf)
                For Each std As Student In stdlst
                    sb.AppendFormat(" {0,-2} {1,-10} {2, -40} {3}", String.Format("{0:00}", stdlst.IndexOf(std) + 1), std.Id, std.Name, vbCrLf)
                Next
                Console.WriteLine(sb.ToString)

            Case "2" ' DESCARGAR ARCHIVOS DE EVALUACION

                Dim ind As Integer
                Console.WriteLine()
                Console.Write("NUMERO DE ORDEN DE ESTUDIANTE: ")
                Integer.TryParse(Console.ReadLine(), ind)

                Dim std As Student = stdlst.Item(ind - 1)

                Dim mdcpth As String = My.Computer.FileSystem.SpecialDirectories.MyDocuments
                Dim crspth As String = IO.Path.Combine(mdcpth, "Pweb162")
                Dim stdpth As String = IO.Path.Combine(crspth, std.Name)

                Console.WriteLine()
                Console.WriteLine(std.Name)

                'Console.WriteLine()
                'Console.WriteLine(" Directory : " & stdpth)
                'Console.WriteLine(" Exist     : " & Directory.Exists(stdpth))

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
                    Console.WriteLine("  [1] PRACTICA 01")
                    Console.WriteLine("  [2] EXAMEN PARCIAL")
                    Console.WriteLine("  [3] PRACTICA 02")
                    Console.WriteLine("  [4] EXAMEN FINAL")
                    Console.WriteLine()
                    Console.Write("INGRESA UNA OPCION: ")
                    inputEval = Console.ReadLine()
                Loop Until isValidEval(inputEval)

                Dim evlpth As String = String.Empty
                Dim evlstr As String = String.Empty

                Select Case inputEval
                    Case "1"
                        evlpth = IO.Path.Combine(stdpth, "E1-PRC01")
                    Case "2"
                        evlpth = IO.Path.Combine(stdpth, "E2-EXPCL")
                    Case "3"
                        evlpth = IO.Path.Combine(stdpth, "E3-PRC02")
                    Case "4"
                        evlpth = IO.Path.Combine(stdpth, "E4-EXFNL")
                End Select

                Console.WriteLine()
                Console.WriteLine(std.Name)
                Console.WriteLine(IO.Path.GetFileName(evlpth))

                'Console.WriteLine()
                'Console.WriteLine(IO.Path.GetDirectoryName(evlpth))
                'Console.WriteLine(evlpth)

                Dim inputServer As String
                Dim isValidServer = Function(x)
                                        If Not Regex.IsMatch(x, "^[12]{1,1}$") Then
                                            Console.WriteLine("OPCION NO VALIDA, INTENTALO DE NUEVO.")
                                            Console.WriteLine()
                                            Return False
                                        End If
                                        Return True
                                    End Function
                Do
                    Console.WriteLine()
                    Console.WriteLine("SERVIDOR DE DESCARGA")
                    Console.WriteLine()
                    Console.WriteLine("  [1] GITHUB")
                    Console.WriteLine("  [2] SLACK")
                    Console.WriteLine()
                    Console.Write("INGRESA UNA OPCION: ")
                    inputServer = Console.ReadLine()
                Loop Until isValidEval(inputServer)

                Console.WriteLine()

                Select Case inputServer

                    Case "1" ' GITHUB

                        Dim rurl As String
                        Console.Write(" URL: ")
                        rurl = Console.ReadLine()
                        Dim rname As String = IO.Path.GetFileNameWithoutExtension(rurl.Replace("https://", ""))
                        Dim rpth As String = IO.Path.Combine(evlpth, rname)
                        Directory.CreateDirectory(rpth)
                        Repository.Clone(rurl, rpth)
                        Diagnostics.Process.Start(evlpth)
                        Console.WriteLine()

                    Case "2" ' SLACK

                        Dim rurl As String
                        Console.Write(" URL: ")
                        rurl = Console.ReadLine()
                        Dim rname As String = IO.Path.GetFileName(rurl.Replace("https://", ""))
                        Dim rpth As String = IO.Path.Combine(evlpth, rname.Split("?")(0))
                        My.Computer.Network.DownloadFile(rurl, rpth)
                        Diagnostics.Process.Start(evlpth)
                        Console.WriteLine()

                End Select

            Case "3" ' GENERAR DIRECTORIOS DE EVALUACIONES
                GenerateStudentDirectories()
                Console.WriteLine()
                Console.WriteLine(" ¡DIRECTORIOS CREADOS SATISFACTORIAMENTE!")
                Console.WriteLine()
        End Select


    End Sub

    Public Shared Sub GenerateStudentDirectories()

        Dim lst = Pweb162M.GetStudents()
        Dim mdcpath = My.Computer.FileSystem.SpecialDirectories.MyDocuments
        Dim crspath = Path.Combine(mdcpath, "Pweb162")
        Directory.CreateDirectory(crspath)

        Dim stpath As String

        For Each std As Student In lst
            stpath = Path.Combine(crspath, std.Name)
            Directory.CreateDirectory(stpath)
            Directory.CreateDirectory(Path.Combine(stpath, "E1-PRC01"))
            Directory.CreateDirectory(Path.Combine(stpath, "E2-EXPCL"))
            Directory.CreateDirectory(Path.Combine(stpath, "E3-PRC02"))
            Directory.CreateDirectory(Path.Combine(stpath, "E4-EXFNL"))
        Next

    End Sub

    Public Shared Sub SmartEval(eval As Evaluation)
    End Sub

    Private Shared Sub SmartEvalP1()
    End Sub

    Private Shared Sub SmartEvalEP()
    End Sub

    Private Shared Sub SmartEvalP2()
    End Sub

    Private Shared Sub SmartEvalEF()
        Console.WriteLine("-----------------------------")
        Console.WriteLine("- INGRESO NOTA EXAMEN FINAL -")
        Console.WriteLine("-   SMART EVALUATION MODE   -")
        Console.WriteLine("-----------------------------")
        Console.WriteLine()
        Console.WriteLine("HOLA")

        Dim serializer As New JsonSerializer() With {.Formatting = Formatting.Indented}
        Dim path As String = IO.Path.Combine(My.Application.Info.DirectoryPath, "sim162-lst-json.txt")
        Dim stdlst = StudentIO.GetStudentsFromJson("sim162-lst-json.txt")
        Dim pdata = StudentIO.GetQuestionData(Evaluation.EF)
        Dim tlst As New List(Of Integer)

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
            Console.WriteLine("MODO DE INGRESO")
            Console.WriteLine()
            Console.WriteLine(" [1] TODOS LOS EXAMENES POR LISTA DESDE EL PRINCIPIO.")
            Console.WriteLine(" [2] TODOS LOS EXAMENES POR LISTA A PARTIR DEL INDICE INGRESADO.")
            Console.WriteLine(" [3] SOLO UN EXAMEN SEGUN NUMERO DE ORDEN INGRESADO.")
            Console.WriteLine()
            Console.Write("INGRESA UNA OPCION: ")
            optn = Console.ReadLine()
        Loop Until (isValidOption(optn))

        Dim ind As Integer = 0

        Select Case optn
            Case "1"
                ' Nothing
            Case "2"
                Console.WriteLine()
                Console.Write("INGRESAR RESPUESTAS A PARTIR DEL INDICE: ")
                Integer.TryParse(Console.ReadLine(), ind)
            Case "3"
                Console.WriteLine()
                Console.Write("INGRESAR RESPUESTAS DE ALUMNO CON NUMERO DE ORDEN: ")
                Integer.TryParse(Console.ReadLine(), ind)
        End Select

        Console.WriteLine()

        For Each std As Student In stdlst

            Select Case optn
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

            Console.WriteLine(std.Name)
            Console.WriteLine()

            Dim grade As Double = 0
            Dim score As Double = 0

            Dim t1 As DateTime
            Dim t2 As DateTime

            Dim ans
            Dim ansStr

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
                t1 = Date.Now()
                For Each preg In pdata
                    Do
                        Console.Write(" " & preg.Name & ControlChars.Tab & "  :   ")
                        ansStr = Console.ReadLine()
                    Loop While (Not isValidInput(ansStr))

                    ans = Convert.ToInt32(ansStr, 2)
                    grade += IIf(ans = preg.Answer, preg.Score, 0)
                    score = IIf(ans = preg.Answer, preg.Score, 0)
                    Console.WriteLine(" SCORE" & ControlChars.Tab & "  :   " & score)
                    Console.WriteLine()
                    Console.Write(ControlChars.Cr)
                Next
                t2 = Date.Now()
                Console.WriteLine()
                Console.WriteLine(std.Name)
                Console.WriteLine("NOTA EXAMEN TEORICO: " & grade)
                Console.Write("CONFIRMAR EVALUACION (SI/NO): ")
            Loop While (Console.ReadLine().Trim().ToUpper() = "NO")

            tlst.Add(t2.Subtract(t1).Seconds)
            std.Grades.Item(Evaluation.EF) = grade
            Using sw As New StreamWriter(path), writer As New JsonTextWriter(sw)
                serializer.Serialize(writer, stdlst)
            End Using
            Console.WriteLine()
        Next
    End Sub

End Class
