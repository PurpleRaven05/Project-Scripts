package com.jorgebg.classprobuilder.student

import com.android.volley.Response
import com.jorgebg.classprobuilder.Model.Model
import com.jorgebg.classprobuilder.databases.StudentProvisional
import com.jorgebg.classprobuilder.databases.Student_Subject_Provisional
import com.jorgebg.classprobuilder.databases.SubjectProvisional
import com.jorgebg.classprobuilder.databases.subject_ponderation_provisional

class presenterStudentSubject(val view :student_signature, val model: Model) {
    var hasPons : Boolean = true
    init {
        updateNames()
        getCoursedSubject()
    }

    fun updateNames(){//nombres recycler

        model.getSubjectsOfStudent(Response.Listener<List<Student_Subject_Provisional>>
        { it ->
            view.initRecycler(it)
        }, Response.ErrorListener {

        }, view.idStudent)
    }

    fun getCoursedSubject(){//nombres recycler

        model.getSubjectsOfStudent(Response.Listener<List<Student_Subject_Provisional>>
        { it ->  view.listCoursedSubjects=it}, Response.ErrorListener {},view.idStudent)
    }

    fun deleteSubjectStudent(studentSubjectID : Int){

        model.removeSubjectOfStudent(studentSubjectID)

    }

    fun insertSubjectToStudent(newSubject: Student_Subject_Provisional){

        model.addSubjectToStudent(newSubject)

    }

    fun getAllSubjects(){

        model.getSubjects(Response.Listener<List<SubjectProvisional>>
        { it ->
            view.getSubjectsComplete(it)
        }, Response.ErrorListener {

        })

    }

    fun checkPonderations(subjectID : Int){

        model.getPondsFromSubject(subjectID,Response.Listener<List<subject_ponderation_provisional>>
        { it ->  if(it.isEmpty())
            hasPons=false
        }, Response.ErrorListener {})

    }


}