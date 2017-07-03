Imports arctic

Public Class ExFinal

    Private _CodStd As String
    Private _Name As String
    Private _Jobs As ProcessLoad
    Private WithEvents _runner As Runner

    Public Property CodStd() As String
        Get
            Return _CodStd
        End Get
        Set(ByVal value As String)
            _CodStd = value
        End Set
    End Property


    Public Property Name() As String
        Get
            Return _Name
        End Get
        Set(ByVal value As String)
            _Name = value
        End Set
    End Property


    Public Property Jobs() As ProcessLoad
        Get
            If _Jobs Is Nothing Then
                _Jobs = New ProcessLoad
            End If
            Return _Jobs
        End Get
        Set(ByVal value As ProcessLoad)
            _Jobs = value
        End Set
    End Property

    Private _Answers As Dictionary(Of Algoritmo, Answer)
    Public Property Answers() As Dictionary(Of Algoritmo, Answer)
        Get
            If _Answers Is Nothing Then
                _Answers = New Dictionary(Of Algoritmo, Answer)
            End If
            Return _Answers
        End Get
        Set(ByVal value As Dictionary(Of Algoritmo, Answer))
            _Answers = value
        End Set
    End Property

    Public Sub New()
    End Sub

    Public Function GenerateJobs()
        Dim _processId As Integer = 0
        Dim random = New Random()

        Dim small, medium, large
        small = 3
        medium = 2
        large = 3
        Dim total As Integer = 12

        ' used as the upper limit when generating the ArrivalTime of a process
        ' generate the processes to satisfy the properties defined by the user.
        For i As Integer = 0 To small - 1
            Jobs.Add(New Process(System.Math.Max(System.Threading.Interlocked.Increment(_processId), _processId - 1), random.[Next](CInt(BurstTime.SmallMin), CInt(BurstTime.SmallMax)), random.[Next](0, total)) With {
                    .Priority = DirectCast(random.[Next](0, 3), Priority)
            })
        Next
        For i As Integer = 0 To medium - 1
            Jobs.Add(New Process(System.Math.Max(System.Threading.Interlocked.Increment(_processId), _processId - 1), random.[Next](CInt(BurstTime.MediumMin), CInt(BurstTime.MediumMax)), random.[Next](0, total)) With {
                    .Priority = DirectCast(random.[Next](0, 3), Priority)
            })
        Next
        For i As Integer = 0 To large - 1
            Jobs.Add(New Process(System.Math.Max(System.Threading.Interlocked.Increment(_processId), _processId - 1), random.[Next](CInt(BurstTime.LargeMin), CInt(BurstTime.LargeMax)), random.[Next](0, total)) With {
                    .Priority = DirectCast(random.[Next](0, 3), Priority)
            })
        Next

        Jobs.Sort(Function(x, y) x.ArrivalTime.CompareTo(y.ArrivalTime))
        Dim index As Integer = 1
        For Each job As Process In Jobs
            job.Id = index
            index += 1
        Next
    End Function

    Public Function RunSimulation(algoritmo As Algoritmo) As Runner

        Dim cpuUtilizations As List(Of Double)
        Dim id As Guid
        Dim large As Integer
        Dim medium As Integer
        Dim processCount As Integer
        Dim repeat As Integer
        Dim responseTimeStandardDeviations As List(Of Double)
        Dim small As Integer
        Dim strategy As IStrategy
        Dim turnaroundTimeStandardDeviations As List(Of Double)
        Dim waitTimeStandardDeviations As List(Of Double)
        Dim throughputs As Pairs(Of Integer, Double)
        Dim timeframe As Integer

        id = Guid.NewGuid()
        small = 2
        medium = 0
        large = 2
        repeat = 1
        processCount = small + medium + large
        waitTimeStandardDeviations = New List(Of Double)()
        cpuUtilizations = New List(Of Double)()
        turnaroundTimeStandardDeviations = New List(Of Double)()
        responseTimeStandardDeviations = New List(Of Double)()
        throughputs = New Pairs(Of Integer, Double)()
        timeframe = 100

        strategy = Nothing

        Select Case algoritmo
            Case Algoritmo.FCFS
                strategy = New FirstComeFirstServed()
            Case Algoritmo.SJN
                strategy = New ShortestJobFirst(0)
            Case Algoritmo.RoundRobin
                strategy = New RoundRobin(5)
        End Select

        _runner = New Runner(Jobs, strategy, timeframe)
        _runner.Run()


        'Dim p1 As New Process(1, 10, 0) With {.Priority = Priority.Medium}
        'Dim p2 As New Process(2, 12, 2) With {.Priority = Priority.Medium}
        'Dim p3 As New Process(3, 3, 3) With {.Priority = Priority.Medium}
        'Dim p4 As New Process(4, 1, 6) With {.Priority = Priority.Medium}
        'Dim p5 As New Process(5, 15, 9) With {.Priority = Priority.Medium}

        'processLoad = New ProcessLoad()
        'processLoad.Add(p1)
        'processLoad.Add(p2)
        'processLoad.Add(p3)
        'processLoad.Add(p4)
        'processLoad.Add(p5)

        'runner = New Runner(small, medium, large, strategy, timeframe)


        'For Each p As Process In runner.ProcessLoad

        '    Console.WriteLine(p.Id)
        '    Console.WriteLine(p.ArrivalTime)
        '    Console.WriteLine(p.BurstTime)
        '    Console.WriteLine(p.CompletionTime)
        '    Console.WriteLine(p.TurnaroundTime)
        '    Console.WriteLine()

        'Next
        'Console.WriteLine()
        'Console.WriteLine(runner.GetBurstTimeMean)
        'Console.WriteLine(runner.GetThroughputMean)
        'Console.WriteLine(runner.GetTurnaroundTimeMean())
        'Console.WriteLine(runner.GetWaitTimeMean)
        'Console.WriteLine(runner.GetResponseTimeMean)

        Return _runner

    End Function


    Friend Sub DisplayStats()
        Throw New NotImplementedException()
    End Sub

    Friend Sub AnswerFCFS()
        Throw New NotImplementedException()
    End Sub

    Public Sub Process_Started(sender As Object, args As ProcessStartedEventArgs) Handles _runner.ProcessStarted
        Console.WriteLine(" EVENTO      : INICIO DE TRABAJO")
        Console.WriteLine(" TRABAJO     : " & args.Id)
        Console.WriteLine(" TIEMPO      : " & args.StartTime)
        Console.WriteLine("")
    End Sub


    Private Sub _runner_ProcessCompleted(sender As Object, e As ProcessCompletedEventArgs) Handles _runner.ProcessCompleted
        Console.WriteLine(" EVENTO      : TRABAJO COMPLETADO")
        Console.WriteLine(" TRABAJO     : " & e.Id)
        Console.WriteLine(" T. ACTUAL   : " & e.CompletionTime)
        Console.WriteLine("")
    End Sub

    Private Sub _runner_ProcessPreempted(sender As Object, e As ProcessPreemptedEventArgs) Handles _runner.ProcessPreempted
        Console.WriteLine(" EVENTO      : TRABAJO INTERRUMPIDO")
        Console.WriteLine(" TRABAJO     : " & e.Id)
        Console.WriteLine(" T. ACTUAL   : " & _runner.Time)
        Console.WriteLine("")
    End Sub

    Private Sub _runner_Completed(sender As Object, e As RunnerCompletedEventArgs) Handles _runner.Completed
        Console.WriteLine(" EVENTO      : SIMULACION TERMINADA")
        Console.WriteLine(" T. ACTUAL   : " & _runner.Time)
        Console.WriteLine("")
    End Sub

    Private Sub _runner_Started(sender As Object, e As RunnerStartedEventArgs) Handles _runner.Started
        Console.WriteLine(" EVENTO      : SIMULACION INICIADA")
        Console.WriteLine(" T. ACTUAL   : " & _runner.Time)
        Console.WriteLine("")
    End Sub

    Private Sub _runner_ProcessResumed(sender As Object, e As ProcessResumedEventArgs) Handles _runner.ProcessResumed
        Console.WriteLine(" EVENTO      : TRABAJO RESUMIDO")
        Console.WriteLine(" TRABAJO     : " & e.Id)
        Console.WriteLine(" T. ACTUAL   : " & _runner.Time)
        Console.WriteLine("")
    End Sub


End Class
