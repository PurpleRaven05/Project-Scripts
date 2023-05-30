package com.jorgebg.classprobuilder.subject

import android.util.Log
import com.android.volley.Response
import com.jorgebg.classprobuilder.Model.Model
import com.jorgebg.classprobuilder.databases.SubjectProvisional
import com.jorgebg.classprobuilder.databases.subject_ponderation_provisional

class presenterSubjectPercentaje(val view : subjectPercentage, val model: Model) {

    init {

        updatePercentageRecycler()
    }


    fun updatePercentageRecycler(){

        model.getPondsFromSubject(view.subjectId,Response.Listener<List<subject_ponderation_provisional>>
        { it ->

            view.initRecycler(it)
        }, Response.ErrorListener {

        })
    }

    fun insertPonderation(nombre:String, peso:Float){

        model.addPonderation(view.subjectId, nombre, peso)
    }

    fun deletePonderation(idPond : Int){

        model.deletePonderation(idPond)

    }







}