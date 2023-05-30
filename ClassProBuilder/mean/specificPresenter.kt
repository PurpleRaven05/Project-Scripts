package com.jorgebg.classprobuilder.mean

import android.util.Log
import android.view.View
import com.android.volley.Response
import com.jorgebg.classprobuilder.Model.Model
import com.jorgebg.classprobuilder.databases.Student_ponderation_provisional
import kotlinx.coroutines.*
import kotlin.math.roundToInt


class specificPresenter(val view : specificMeanCopy, val model: Model) {

    init {

        GlobalScope.launch{

            calculateMarks(view.studId, view.subId)

        }

    }

    fun calculateMarks(studentId:Int,subjectId:Int){

        model.getExistingStudentPonderations(Response.Listener<List<Student_ponderation_provisional>>{
            it->
            var listOfPond = it
            Log.d("Lista de ponderacion",listOfPond.toString())
            var listOfMarks = ArrayList<Float>()
            for(item in listOfPond){
                if(item.nota != null){
                    listOfMarks.add(item.nota)
                    Log.d("Lista de notas",listOfMarks.toString())
                }
            }
            if(listOfMarks.isNotEmpty()){
                model.getMean(studentId,subjectId, Response.Listener<Float>{
                    it->view.mean(it) },Response.ErrorListener{})
            }else
                view.mean(0.toFloat())
        },Response.ErrorListener{},studentId,subjectId,)



    }
}