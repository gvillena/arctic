
Imports System.IO
Imports System.Text
Imports System.Text.RegularExpressions
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
        Dim stdlst = StudentIO.GetStudents("pweb162-lst-json.txt")

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
            Console.WriteLine(" [1] MOSTRAR LISTA")
            Console.WriteLine(" [2] MOSTRAR LISTA + NOTAS")
            Console.WriteLine(" [3] MOSTRAR LISTA + PROMEDIO")
            Console.WriteLine(" [4] MOSTRAR LISTA + NOTAS + PROMEDIO")
            Console.WriteLine(" [5] INGRESAR NOTAS")
            Console.WriteLine()
            Console.Write("INGRESA UNA OPCION: ")
            optn = Console.ReadLine()
        Loop Until (isValidOption(optn))

        Dim ind As Integer = 0

        Select Case optn

            Case "1" ' MOSTRAR LISTA

                Dim sb As New StringBuilder()
                sb.AppendLine()
                sb.AppendFormat("  {0,-10}  {1, -40}", "Id", "Name", vbCrLf)

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
                        sb.AppendFormat("  {0,-10}  {1, -40} {2, -4} {2}", "Id", "Name", "P1", vbCrLf)
                        Console.WriteLine(sb.ToString())
                    Case "2"
                    Case "3"
                    Case "4"

                End Select

                'sb.AppendFormat("  {0,-10}  {1, -40} P1  EP  P2  EF  PF {2}", "Id", "Name", vbCrLf)

                'For Each std As Student In stdlst
                '    sb.AppendFormat("  {0,-10} {1, -40} {2:00} {}", std.Id, std.Name,
                '                                                    std.Grades(Evaluation.P1),
                '                                                    std.Grades(Evaluation.EP),
                '                                                    std.Grades(Evaluation.P2),
                '                                                    std.Grades(Evaluation.EF),
                '                                                    std.FinalGrade,
                '                                                    vbCrLf)
                'Next
                'Console.WriteLine()
                'Console.Write("INGRESAR RESPUESTAS A PARTIR DEL INDICE: ")
                'Integer.TryParse(Console.ReadLine(), ind)
            Case "3"
                'Console.WriteLine()
                'Console.Write("INGRESAR RESPUESTAS DE ALUMNO CON NUMERO DE ORDEN: ")
                'Integer.TryParse(Console.ReadLine(), ind)
        End Select

        Console.WriteLine()

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
