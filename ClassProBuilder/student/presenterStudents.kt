package com.jorgebg.classprobuilder.student

import com.android.volley.Response
import com.jorgebg.classprobuilder.Model.Model
import com.jorgebg.classprobuilder.databases.StudentProvisional

class presenterStudents(val view :student_menu, val model: Model) {


    init {
       updateNames()
    }


    fun updateNames(){

        model.getStudents(Response.Listener<List<StudentProvisional>>
        { it ->
            view.initRecycler(it)
        }, Response.ErrorListener {

        })

    }
    fun insertStudent(student: StudentProvisional){

        model.addStudents(student)

    }

    fun deleteStudent(nameStudent : String){

        model.removeStudent(nameStudent)

    }

}