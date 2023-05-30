package com.jorgebg.classprobuilder

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.view.View
import com.jorgebg.classprobuilder.mean.meanSubject
import com.jorgebg.classprobuilder.student.student_menu
import com.jorgebg.classprobuilder.subject.subject_menu

class MainMenu : AppCompatActivity() {

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main_menu)
    }

    fun accessSignatures(view: View) {

        val intent = Intent(applicationContext, subject_menu::class.java)
        startActivity(intent)

    }
    fun accessStudents(view: View) {

        val intent = Intent(applicationContext, student_menu::class.java)
        startActivity(intent)


    }
    fun accessMarks(view: View) {

        val intent = Intent(applicationContext, meanSubject::class.java)
        startActivity(intent)


    }
}