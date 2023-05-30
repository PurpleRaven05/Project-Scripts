package com.jorgebg.classprobuilder.student

import android.app.ProgressDialog.show
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.util.Log
import android.view.View
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView
import com.jorgebg.classprobuilder.Model.Model
import com.jorgebg.classprobuilder.R
import com.jorgebg.classprobuilder.databases.StudentProvisional

class student_menu : AppCompatActivity() {

    lateinit var presenter : presenterStudents
    lateinit var model : Model

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_student_menu)

        model = Model(applicationContext)
        presenter = presenterStudents(this , model)


    }

    fun initRecycler(lista : List<StudentProvisional>){

        val recyclerStudentsName : RecyclerView = findViewById(R.id.recyclerStudentName)

        recyclerStudentsName.layoutManager = LinearLayoutManager(this)

        val adapter = recyclerAdapterStudents(lista, this)

        recyclerStudentsName.adapter = adapter

    }

    fun showDialog(nameToShow: String, surnameToShow:String, selected_id : Int){

        Log.d("Testeo id 2", (selected_id).toString() )

        dialogNames(nameToShow,surnameToShow,selected_id,presenter).apply{
            show(supportFragmentManager, "dialog")
            setRetainInstance(true)
        }
    }

    fun insertStudent(view: View) {

        dialogInsertName(presenter).apply{
            show(supportFragmentManager, "dialog")
            setRetainInstance(true)
        }

    }


}