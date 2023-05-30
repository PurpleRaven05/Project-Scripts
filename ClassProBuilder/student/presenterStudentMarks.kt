package com.jorgebg.classprobuilder.student


import com.android.volley.Response
import com.jorgebg.classprobuilder.Model.Model
import com.jorgebg.classprobuilder.databases.Student_Subject_Provisional
import com.jorgebg.classprobuilder.databases.Student_ponderation_provisional
import com.jorgebg.classprobuilder.databases.subject_ponderation_provisional
import kotlinx.coroutines.GlobalScope
import kotlinx.coroutines.delay
import kotlinx.coroutines.launch


class presenterStudentMarks(val view :student_marks, val model: Model) {
    val isAssigned = true

    init {
        GlobalScope.launch {
            delay(250)
            updateMarks()
            getPonderationsAsigned()
        }

    }

    fun updateMarks(){

        model.getStudentPonderations(Response.Listener<List<Student_ponderation_provisional>>
        { it ->
            view.initRecycler(it)
        }, Response.ErrorListener {

        },view.idStudent,view.idStudentSubject)

    }

    fun getPonderationsAsigned(){

        model.getStudentPonderations(Response.Listener<List<Student_ponderation_provisional>>
        { it -> view.listAsignedPonderations=it}, Response.ErrorListener {},view.idStudent,view.idStudentSubject)

    }

    fun insertPondToStudent(newPond: Student_ponderation_provisional){

        model.insertPonderationStudent(newPond)

    }


    fun getAllPonderations(){

        model.getPondsFromSubject( view.idSubject,Response.Listener<List<subject_ponderation_provisional>>
        { it ->
            view.getAllponderations(it)
        }, Response.ErrorListener {

        },)

    }

    fun deleteStudentPonderation(id:Int){
        model.deleteStudentPonderation(id)
    }






}