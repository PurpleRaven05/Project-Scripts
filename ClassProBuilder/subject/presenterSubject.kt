package com.jorgebg.classprobuilder.subject

import android.util.Log
import com.android.volley.Response
import com.jorgebg.classprobuilder.Model.Model
import com.jorgebg.classprobuilder.databases.SubjectProvisional

class presenterSubject(val view : subject_menu, val model: Model) {

    init {

        updateSubjectRecycler()
    }


    fun updateSubjectRecycler(){

        model.getSubjects(Response.Listener<List<SubjectProvisional>>
        { it ->view.initRecycler(it)
        }, Response.ErrorListener {})

    }
    fun insertSubject(nameSubject : String){

        model.addSubject(nameSubject)

    }

    fun deleteSubject(IDSubject : Int){

        model.removeSubject(IDSubject)

    }


}