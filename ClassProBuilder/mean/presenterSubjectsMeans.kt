package com.jorgebg.classprobuilder.mean

import android.util.Log
import androidx.lifecycle.Lifecycle
import com.android.volley.Response
import com.jorgebg.classprobuilder.Model.Model
import com.jorgebg.classprobuilder.databases.Student_Subject_Provisional
import kotlinx.coroutines.*

class presenterSubjectsMeans(val view : recyclerSubjectMeans, val model: Model) {

    lateinit var listOfWeights: List<Float>
    lateinit var listOfMarks: List<Float>
    var result = 0.0

    var listaAsignaturas : List<Student_Subject_Provisional> = emptyList()

    var finalList : ArrayList<String> = ArrayList()
    var finalList2 : ArrayList<String> = ArrayList()
    init {
        getSubjectnames()

       generateList(listaAsignaturas)
    }


    fun getSubjectnames(){//nombres recycler

        model.getSubjectsOfStudent(Response.Listener<List<Student_Subject_Provisional>>
        { it ->
            listaAsignaturas=it
        }, Response.ErrorListener {

        }, view.studId)
    }


    fun getWeights(studentId:Int,subjectId:Int){
        model.getAllWeights(studentId,subjectId, Response.Listener<List<Float>>{
            it->
            listOfWeights=it
        },Response.ErrorListener{

        },)
    }
    fun getMarks(studentId:Int,subjectId:Int){

        model.getAllMarks(studentId,subjectId, Response.Listener<List<Float>>{
            it->
            listOfMarks=it

        },Response.ErrorListener{

        },)
    }
    fun calculateMarks(){
        for(item in listOfMarks.indices ){
            result = result +(listOfMarks[item]*(listOfWeights[item]/100))
        }
        view.result = result
       // view.MarkText.text=(result.toFloat()).toString()
    }

    fun generateList(subject: List<Student_Subject_Provisional>) {

        GlobalScope.launch{


            delay(250)
            Log.d("Testeo lista asignaturas", listaAsignaturas.toString())

            for(item in listaAsignaturas){

                getWeights(view.studId, item.id)
                delay(250)

                getMarks(view.studId,item.id)
                delay(250)
                result=0.0
                calculateMarks()

                delay(250)
                var aux : String = item.nombre.toString()
                var aux2: String = result.toFloat().toString()


                finalList.add(aux)
                finalList2.add(aux2)
                Log.d("RESULTADO", finalList.toString())
            }
            launch(Dispatchers.Main.immediate){
                Log.d("END", finalList.toString())
                view.initRecycler(finalList,finalList2)
            }
        }
    }

}