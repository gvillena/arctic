Imports System.IO
Imports System.Text
Imports System.Text.RegularExpressions
Imports CommandLine
Imports CommandLine.Text
Imports Newtonsoft.Json

Module Main

    Sub Main()

        ' Declare and initialize parser 
        Dim parser = New Parser(Sub(config) config.HelpWriter = Console.Out)

        ' Parse result
        Dim args = My.Application.CommandLineArgs
        Dim result = parser.ParseArguments(Of Pweb162Options,
                                              Sim162Options,
                                              So162Options,
                                              Visual162Options,
                                              TestOptions)(args)

        ' Execute parsed action
        result.WithParsed(Of Pweb162Options)(AddressOf Pweb162).
                WithParsed(Of Sim162Options)(AddressOf Sim162).
                WithParsed(Of So162Options)(AddressOf So162).
                WithParsed(Of Visual162Options)(AddressOf Visual162).
                WithParsed(Of TestOptions)(AddressOf Test).
                WithNotParsed(Function(errors) 1)

    End Sub

    Function Pweb162(opts As Pweb162Options)
        Return 0
    End Function

    Function Sim162(opts As Sim162Options)

        Dim opc As String = String.Empty

        Console.WriteLine("-----------------------------")
        Console.WriteLine("Simulacion de Sistemas 2016-2")
        Console.WriteLine("-----------------------------")
        Console.WriteLine()
        Console.WriteLine("[1] Lista")
        Console.WriteLine("[2] Notas P1")
        Console.WriteLine("[3] Notas EP")
        Console.WriteLine("[4] Notas P2")
        Console.WriteLine("[5] Notas EF")
        Console.WriteLine("[6] Notas P1 / EP / P2 / EF")
        Console.WriteLine("[7] Promedios Finales")
        Console.WriteLine("[8] Promedios Finales (Notas)")
        Console.WriteLine("[9] Promedios Finales (Notas / Susti)")
        Console.WriteLine("[10] Salir")

        Console.WriteLine()
        Console.Write("Seleccione una opcion: ")
        opc = Console.ReadLine()

        Select Case opc

            ' Mostrar Lista
            Case "1"

                Dim sb = New StringBuilder()
                Dim stdlst = StudentIO.GetStudentsJson("sim162-lst-json.txt")

                sb.AppendFormat(" {0,-10}  {1, -40} {2}", "CODIGO", "APELLIDOS Y NOMBRES", vbCrLf)
                For Each std As Student In stdlst
                    sb.AppendFormat(" {0,-10}  {1, -40} {2}", std.Id, std.Name, vbCrLf)
                Next
                Console.WriteLine()
                Console.WriteLine(sb.ToString())

            Case "2"

                Console.WriteLine("NOTAS PRACTICA 01")
                Console.WriteLine()
                Console.WriteLine("[1] Mostrar")
                Console.WriteLine("[2] Ingresar")
                Console.WriteLine("[3] Modificar")
                Console.WriteLine("[4] Salir")
                Console.WriteLine()
                Console.Write("Seleccione una opcion: ")
                opc = Console.ReadLine()

                Select Case opc
                    Case 1
                        Dim sb = New StringBuilder()
                        Dim stdlst = StudentIO.GetStudentsJson("sim162-lst-json.txt")
                        sb.AppendFormat(" {0,-10}  {1, -40} {2:00.00} {3}", "CODIGO", "APELLIDOS Y NOMBRES", "P1", vbCrLf)

                        For Each std As Student In stdlst
                            sb.AppendFormat(" {0,-10}  {1, -40} {2:00.00} {3}", std.Id, std.Name,
                                                                                std.Grades(Evaluation.P1),
                                                                                vbCrLf)
                        Next
                        Console.WriteLine()
                        Console.WriteLine(sb.ToString())

                    Case 2

                        Console.WriteLine("INGRESAR NOTAS PRACTICA 01")
                        Console.WriteLine("--------------------------")
                        Console.WriteLine("MODOS DE INGRESO:")
                        Console.WriteLine()
                        Console.WriteLine(" [1] Normal")
                        Console.WriteLine(" [2] SmartEval")
                        Console.WriteLine(" [3] Salir")
                        Console.WriteLine()
                        Console.Write("Seleccione un modo de ingreso: ")
                        opc = Console.ReadLine()

                        Select Case opc
                            Case 1
                                Console.Write("Ingrese codigo de alumno o presione 'Enter' para todos los alumnos: ")
                                If String.IsNullOrEmpty(opc) Then

                                Else
                                    ' TODO Student One By One
                                End If
                            Case 2

                            Case Else

                        End Select



                    Case Else
                        Console.WriteLine("Opcion no valida... :(")
                End Select

                'Dim sb = New StringBuilder()
                'Dim stdlst = StudentIO.GetStudentsJson("sim162-lst-json.txt")
                'sb.AppendFormat(" {0,-10}  {1, -40} {2:00.00} {3:00.00} {4:00.00} {5:00.00} {6}", "CODIGO",
                '                                                                                  "APELLIDOS Y NOMBRES",
                '                                                                                  "P1", "EP", "P2", "EF",
                '                                                                                  vbCrLf)
                'For Each std As Student In stdlst
                '    sb.AppendFormat(" {0,-10}  {1, -40} {2:00.00} {3:00.00} {4:00.00} {5:00.00} {6}", std.Id,
                '                                                                                      std.Name,
                '                                                                                      std.Grades(Evaluation.P1),
                '                                                                                      std.Grades(Evaluation.EP),
                '                                                                                      std.Grades(Evaluation.P2),
                '                                                                                      std.Grades(Evaluation.EF),
                '                                                                                      vbCrLf)
                'Next
                'Console.WriteLine()
                'Console.WriteLine(sb.ToString())

            Case "3"
                Console.WriteLine("TODO")
            Case "4"
                Console.WriteLine("TODO")
            Case Else
                Console.WriteLine("Opcion no valida... :(")
        End Select
        Return 0
    End Function

    Function So162(opts As So162Options)
        Return 0
    End Function

    Function Visual162(opts As Visual162Options)
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

        Dim serializer As New JsonSerializer() With {.Formatting = Formatting.Indented}
        Dim stdpath As String = Path.Combine(My.Application.Info.DirectoryPath, "so162-lst-json.txt")
        Dim stdlst As List(Of Student) = So162M.GetStudents()
        Dim exfpath As String = Path.Combine(My.Application.Info.DirectoryPath, "so162-exfinal-json.txt")
        Dim exfinal As ExFinal = Nothing

        If Not File.Exists(exfpath) Then
            Dim std As Student = Nothing
            Do
                Console.WriteLine()
                Console.Write("INGRESE SU CODIGO: ")
                std = stdlst.Find(Function(x) x.Id = Console.ReadLine())
                If std Is Nothing Then
                    Console.WriteLine("CODIGO NO VALIDO, INTENTELO DE NUEVO.")
                End If
            Loop While std Is Nothing

            exfinal = New ExFinal()
            exfinal.CodStd = std.Id
            exfinal.Name = std.Name
            exfinal.GenerateJobs()

            Using sw As New StreamWriter(exfpath), writer As New JsonTextWriter(sw)
                serializer.Serialize(writer, exfinal)
            End Using
        Else
            Using file As New StreamReader(exfpath)
                exfinal = serializer.Deserialize(file, GetType(ExFinal))
            End Using
        End If

        Console.WriteLine()
        Console.WriteLine("EXAMEN FINAL SISTEMAS OPERATIVOS 2016-2")
        Console.WriteLine(exfinal.Name)
        Console.WriteLine()
        Console.WriteLine("INFORMACION DE TRABAJOS: ")
        Console.WriteLine()
        Console.WriteLine(" {0,-7}   {1, -9}   {2, -10}", "TRABAJO", "CICLO CPU", "T. LLEGADA")
        For Each p As Process In exfinal.Jobs
            Console.WriteLine(" {0,-7}   {1, -9}   {2, -10}", p.Id, p.BurstTime, p.ArrivalTime)
        Next
        Console.WriteLine()

        Dim optn As String
        Dim isValidOption = Function(x)
                                If Not Regex.IsMatch(x, "^[12]{1,1}$") Then
                                    Console.WriteLine("OPCION NO VALIDA, INTENTALO DE NUEVO.")
                                    Console.WriteLine()
                                    Return False
                                End If
                                Return True
                            End Function

        Do
            Console.WriteLine()
            Console.WriteLine("ALGORITMOS DE PLANIFICACION")
            Console.WriteLine()
            Console.WriteLine(" [1] FCFS")
            Console.WriteLine(" [2] ROUND ROBIN (QTime: 5ms)")
            Console.WriteLine()
            Console.Write("INGRESA UNA OPCION: ")
            optn = Console.ReadLine()
        Loop Until (isValidOption(optn))

        Dim title As String = String.Empty
        Dim algoritmo As Algoritmo = Algoritmo.Ninguno

        Select Case optn
            Case "1" ' FCFS
                title = "FIRST COME FIRST SERVED"
                algoritmo = Algoritmo.FCFS
            Case "2" ' Round Robin
                title = "ROUND ROBIN (QTime: 5ms)"
                algoritmo = Algoritmo.RoundRobin
        End Select

        Do
            Console.WriteLine()
            Console.WriteLine(title)
            Console.WriteLine()
            Console.WriteLine(" [1] EJECUTAR SIMULACION")
            Console.WriteLine(" [2] INFO. ESTADISTICAS")
            'Console.WriteLine(" [3] INGRESAR RESPUESTAS")
            Console.WriteLine()
            Console.Write("INGRESA UNA OPCION: ")
            optn = Console.ReadLine()
        Loop Until (isValidOption(optn))

        Console.WriteLine()

        Select Case optn
            Case "1" ' Ejecutar Simulacion
                Dim runner As Runner
                runner = exfinal.RunSimulation(algoritmo)
            Case "2" ' Ver Estadisticas
                Console.WriteLine()
                Console.WriteLine()
                Console.WriteLine("ESTADISTICAS")
                Console.WriteLine()
                Console.WriteLine(" Utilización de CPU")
                Console.WriteLine(" Turnaround Time")
                Console.WriteLine(" Tiempo de Espera")
                Console.WriteLine(" Tiempo de Respuesta")
            Case "3" ' Ingresar Respuestas

        End Select


        'Select Case optn
        '    Case "1"


        '    Case "2"
        '        exfinal.DisplayStats()
        '    Case "3"
        '        exfinal.AnswerFCFS()
        'End Select






        'Do
        '    Console.WriteLine()
        '    Console.WriteLine("FIRST COME FIRST SERVED")
        '    Console.WriteLine()
        '    Console.WriteLine(" [1] EJECUTAR SIMULACION")
        '    Console.WriteLine(" [2] INFO. ESTADISTICAS")
        '    Console.WriteLine(" [3] INGRESAR RESPUESTAS")
        '    Console.WriteLine()
        '    Console.Write("INGRESA UNA OPCION: ")
        '    optn = Console.ReadLine()
        'Loop Until (isValidOption(optn))


        'Console.WriteLine("Utilice este programa para realizar simulaciones e ingresar de planificacion de trabajos.")

        ''        Console.WriteLine("")
        ''        Console.WriteLine("Calcular utilización de CPU, rendimiento, 'turnaroundtime', tiempo de esperay  tiempo de respuestapara los siguientes algoritmos de planificación de trabajos:
        ''")

        ''        Console.WriteLine("Calcular utilización de CPU, rendimiento, 'turnaroundtime', tiempo de esperay  tiempo de respuestapara los siguientes algoritmos de planificación de trabajos:
        ''")

        'Dim optn As String
        'Select Case optn
        '    Case "1"
        '        exfinal.RunSimulationFCFS()
        '    Case "2"
        '        exfinal.RunSimulationSJN()
        '    Case "3"
        '        exfinal.RunSimulationRoundRobin()
        '    Case "3"
        '        exfinal.RunSimulationRoundRobin()
        '    Case "3"
        '        exfinal.RunSimulationRoundRobin()
        'End Select

        'Dim optn As String
        'Dim isValidOption = Function(x)
        '                        If Not Regex.IsMatch(x, "^[123]{1,1}$") Then
        '                            Console.WriteLine("OPCION NO VALIDA, INTENTALO DE NUEVO.")
        '                            Console.WriteLine()
        '                            Return False
        '                        End If
        '                        Return True
        '                    End Function

        'Do
        '    Console.WriteLine("MENU")
        '    Console.WriteLine()
        '    Console.WriteLine(" [1] TODOS LOS EXAMENES POR LISTA DESDE EL PRINCIPIO.")
        '    Console.WriteLine(" [2] TODOS LOS EXAMENES POR LISTA A PARTIR DEL INDICE INGRESADO.")
        '    Console.WriteLine(" [3] SOLO UN EXAMEN SEGUN NUMERO DE ORDEN INGRESADO.")
        '    Console.WriteLine()
        '    Console.Write("INGRESA UNA OPCION: ")
        '    optn = Console.ReadLine()
        'Loop Until (isValidOption(optn))



        'Console.WriteLine(p.ArrivalTime)
        'Console.WriteLine(p.BurstTime)
        'Console.WriteLine(p.CompletionTime)
        'Console.WriteLine(p.TurnaroundTime)




        Return 0
    End Function



End Module
