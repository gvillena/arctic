﻿Public Class Student

    Private _Id As String
    Private _Name As String
    Private _Grades As Dictionary(Of Evaluation, Double)

    Public Sub New()
        _Id = String.Empty
        _Name = String.Empty
        _Grades = New Dictionary(Of Evaluation, Double)
    End Sub

    Public Property Id() As String
        Get
            Return _Id
        End Get
        Set(ByVal value As String)
            _Id = value
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

    Public Property Grades() As Dictionary(Of Evaluation, Double)
        Get
            Return _Grades
        End Get
        Set(ByVal value As Dictionary(Of Evaluation, Double))
            _Grades = value
        End Set
    End Property

    Public ReadOnly Property FinalGrade() As Double
        Get
            Dim p1, ep, p2, ef
            p1 = _Grades(Evaluation.P1)
            ep = _Grades(Evaluation.EP)
            p2 = _Grades(Evaluation.P2)
            ef = _Grades(Evaluation.EF)
            Return (p1 + ep + p2 + ef) / 4
        End Get
    End Property
End Class

Public Enum Evaluation
    P1
    EP
    P2
    EF
End Enum

Public Enum Alternativa

    A = 8
    B = 4
    C = 2
    D = 1

    Proposito = 8
    Ventaja = 4
    Desventaja = 2
    OtrasCons = 1

    Entidad = 8
    Cola = 4
    Recurso = 2
    Ninguno = 1

    NoAnswer = 0
End Enum


Public Enum Course
    pweb162
    sim162
    so162
    visual162
End Enum

