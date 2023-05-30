package com.jorgebg.classprobuilder.mean

import com.android.volley.Response
import com.jorgebg.classprobuilder.Model.Model
import com.jorgebg.classprobuilder.databases.StudentProvisional
import com.jorgebg.classprobuilder.databases.SubjectProvisional

class presenterMean(val view : meanSubject, val model : Model){

    init {
        getStudents()
        getSubjects()
    }


    fun getStudents(){

        model.getStudents(Response.Listener<List<StudentProvisional>>
        { it ->
            view.setSpinnerAlumno(it)
        }, Response.ErrorListener {

        })

    }
    fun getSubjects(){

        model.getSubjects(Response.Listener<List<SubjectProvisional>>
        { it ->view.setSpinnerAsignaturas(it)
        }, Response.ErrorListener {})

    }

    fun searchStudentOnSubject(studentID : Int, subjectID : Int){

        model.searchStudentOnSubject(Response.Listener<Int>
        { it ->if(it != null)
            view.goMeanSpecific()

        }, Response.ErrorListener {},studentID, subjectID)

    }




}