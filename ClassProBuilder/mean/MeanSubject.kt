package com.jorgebg.classprobuilder.mean

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.util.Log
import android.view.View
import android.widget.ArrayAdapter
import android.widget.Spinner
import com.jorgebg.classprobuilder.Model.Model
import com.jorgebg.classprobuilder.R
import com.jorgebg.classprobuilder.databases.StudentProvisional
import com.jorgebg.classprobuilder.databases.SubjectProvisional
import com.jorgebg.classprobuilder.subject.subject_menu

class meanSubject : AppCompatActivity() {


    lateinit var spinerAlumos : Spinner
    lateinit var listaAlumnos : List<StudentProvisional>
    lateinit var chosenAlumo : StudentProvisional
    lateinit var spinerAsignaturas: Spinner
    lateinit var listaAsignaturas : List<SubjectProvisional>
    lateinit var chosenAsignatura : SubjectProvisional
    var subId=0
    var studId = 0
    lateinit var subName:String
    lateinit var studName:String

    lateinit var presenter : presenterMean
    lateinit var model : Model

    lateinit var newList : ArrayList<String>
    var studentExist = false
    var subjectExist = false
    var isOnSubject = false

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_mean_subject)

        model = Model(applicationContext)
        presenter= presenterMean(this,model)

        spinerAlumos=findViewById(R.id.spinerAlumnos)
        spinerAsignaturas=findViewById(R.id.spinerAsignaturas)

        listaAlumnos= emptyList()




    }

    fun setSpinnerAlumno(lista : List<StudentProvisional>){

        if(lista.isNotEmpty()){
            listaAlumnos=lista
            studentExist = true
        }
        else{
            listaAlumnos= listOf(StudentProvisional(0,"None","",""))
            studentExist = false
        }


        Log.d("Testeo nombre", listaAlumnos.toString())

        newList= arrayListOf()

        for (element in listaAlumnos){
            newList.add(element.nombre)


        }

        val adapter = ArrayAdapter(applicationContext, android.R.layout.simple_dropdown_item_1line, newList)

        spinerAlumos.apply {
            setAdapter(adapter)
        }



    }

    fun setSpinnerAsignaturas(lista : List<SubjectProvisional>){

        if(lista.isNotEmpty()){
            listaAsignaturas=lista
            subjectExist = true
        }
        else{
            listaAsignaturas= listOf(SubjectProvisional(0,"None",0))
            subjectExist = false
        }


        Log.d("Testeo nombre", listaAlumnos.toString())

        newList= arrayListOf()

        for (element in listaAsignaturas){
            newList.add(element.nombre)


        }

        val adapter = ArrayAdapter(applicationContext, android.R.layout.simple_dropdown_item_1line, newList)

        spinerAsignaturas.apply {
            setAdapter(adapter)
        }

    }

    fun MeanStudentSubject(view: View) {

        if(subjectExist && studentExist) {

            val subjectIndex = spinerAsignaturas.selectedItemPosition

            if (subjectIndex != -1)
                chosenAsignatura = listaAsignaturas[subjectIndex]
            else
                chosenAsignatura = SubjectProvisional(0, "Error", 0)

            //
            val studentIndex = spinerAlumos.selectedItemPosition

            if (studentIndex != -1)
                chosenAlumo = listaAlumnos[studentIndex]
            else
                chosenAlumo = StudentProvisional(0, "Error", "None", "None")

            presenter.searchStudentOnSubject(chosenAlumo.id, chosenAsignatura.id)

        }

    }

    fun getSubjectMeans(view: View) {
        if(subjectExist && studentExist){
            val subjectIndex = spinerAsignaturas.selectedItemPosition

            if (subjectIndex!=-1)
                chosenAsignatura = listaAsignaturas[subjectIndex]
            else
                chosenAsignatura = SubjectProvisional(0,"Error",0)
            Log.d("Index1",subjectIndex.toString())

            val studentIndex = spinerAlumos.selectedItemPosition
            if(studentIndex!=-1)
                chosenAlumo = listaAlumnos[studentIndex]
            else
                chosenAlumo = StudentProvisional(0,"Error","None","None")
            Log.d("Index2",studentIndex.toString())



            val intent = Intent(applicationContext, recyclerSubjectMeans::class.java).apply{
                subId=chosenAsignatura.id
                studId=chosenAlumo.id
                subName = chosenAsignatura.nombre
                studName = chosenAlumo.nombre
                putExtra("idSubject",subId)
                putExtra("idStudent",studId)
                putExtra("StudentName",studName)
                putExtra("SubjectName",subName)
                Log.d("Index3",chosenAlumo.toString())
                Log.d("Index4",chosenAsignatura.toString())

            }
            startActivity(intent)
        }

    }

    fun getStudentsMeans(view: View) {
        if(subjectExist && studentExist){
            val subjectIndex = spinerAsignaturas.selectedItemPosition

            if (subjectIndex!=-1)
                chosenAsignatura = listaAsignaturas[subjectIndex]
            else
                chosenAsignatura = SubjectProvisional(0,"Error",0)
            Log.d("Index1",subjectIndex.toString())

            val studentIndex = spinerAlumos.selectedItemPosition
            if(studentIndex!=-1)
                chosenAlumo = listaAlumnos[studentIndex]
            else
                chosenAlumo = StudentProvisional(0,"Error","None","None")
            Log.d("Index2",studentIndex.toString())



            val intent = Intent(applicationContext, studeantSubjectMean::class.java).apply{
                subId=chosenAsignatura.id
                studId=chosenAlumo.id
                subName = chosenAsignatura.nombre
                studName = chosenAlumo.nombre
                putExtra("idSubject",subId)
                putExtra("idStudent",studId)
                putExtra("StudentName",studName)
                putExtra("SubjectName",subName)
                Log.d("Index3",chosenAlumo.toString())
                Log.d("Index4",chosenAsignatura.toString())

            }
            startActivity(intent)
        }


    }

    fun goMeanSpecific(){


            val intent = Intent(applicationContext, specificMeanCopy::class.java).apply{
                subId=chosenAsignatura.id
                studId=chosenAlumo.id
                subName = chosenAsignatura.nombre
                studName = chosenAlumo.nombre
                putExtra("idSubject",subId)
                putExtra("idStudent",studId)
                putExtra("StudentName",studName)
                putExtra("SubjectName",subName)
            }
            startActivity(intent)
    }

}