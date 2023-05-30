package com.jorgebg.classprobuilder.mean

import android.util.Log
import com.android.volley.Response
import com.jorgebg.classprobuilder.Model.Model
import com.jorgebg.classprobuilder.databases.Student_Subject_Provisional
import com.jorgebg.classprobuilder.databases.Student_ponderation_provisional
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.GlobalScope
import kotlinx.coroutines.delay
import kotlinx.coroutines.launch

class presenterStudentsMeans(val view : studeantSubjectMean, val model: Model) {

    lateinit var listOfWeights: List<Float>
    lateinit var listOfMarks: List<Float>
    var result = 0.0

    var listaAlumnos : List<Student_Subject_Provisional> = emptyList()

    var finalList : ArrayList<String> = ArrayList()
    var finalList2 : ArrayList<String> = ArrayList()
    init {


        generateList()
    }


    fun getStudentsNames(){//nombres recycler

        model.getAllSubjectsOfStudent(Response.Listener<List<Student_Subject_Provisional>>
        { it ->
            listaAlumnos=it
        }, Response.ErrorListener {

        }, view.subId)
    }
    fun generateList() {

        GlobalScope.launch{
            Log.d("ASIGNATURA", view.subjectName.toString())
            Log.d("ID", view.subId.toString())

            getStudentsNames()
            delay(250)

            Log.d("LISTA ALUMNOS", listaAlumnos.toString())


            for(item in listaAlumnos){

                //
                model.getExistingStudentPonderations(Response.Listener<List<Student_ponderation_provisional>>{
                    it->
                    var listOfPond = it
                    var listOfMarks = ArrayList<Float>()
                    for(item in listOfPond){
                        if(item.nota != null){
                            listOfMarks.add(item.nota)
                        }
                    }
                    if(listOfMarks.isNotEmpty()){
                        result=0.0
                        model.getMean(item.studentId,item.subjectId, Response.Listener<Float>{
                            it-> result=it.toDouble()},Response.ErrorListener{})

                    }else
                        result=0.toDouble()
                },Response.ErrorListener{},item.studentId,view.subId,)
                //

                delay(200)

                var aux : String = item.nombre

                model.getStudentFromId(item.studentId, Response.Listener<String>{
                    it-> aux=it },Response.ErrorListener{})

                delay(50)

                finalList.add(aux)
                finalList2.add(result.toString())
                Log.d("RESULTADO", finalList.toString())
            }
            launch(Dispatchers.Main.immediate){
                view.initRecycler(finalList,finalList2)
            }
        }
    }

}