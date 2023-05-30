package com.jorgebg.classprobuilder.mean

import android.util.Log
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.TextView
import androidx.recyclerview.widget.RecyclerView
import com.jorgebg.classprobuilder.R
import com.jorgebg.classprobuilder.databases.StudentProvisional
import com.jorgebg.classprobuilder.student.recyclerAdapterStudents
import com.jorgebg.classprobuilder.student.student_menu

class recyclerAdapterSubjMean(val names: List<String>, val marks: List<String>, val view: recyclerSubjectMeans) : RecyclerView.Adapter<recyclerAdapterSubjMean.viewHolder>(){

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): viewHolder {

        val layoutInflater = LayoutInflater.from(parent.context)
        return viewHolder(layoutInflater.inflate(R.layout.recycler_student_marks, parent, false))

    }

    override fun onBindViewHolder(holder: viewHolder, position: Int) {

        with(names[position]){
            holder.nombre.text = names[position]
            holder.mark.text = marks[position]

        }

    }

    override fun getItemCount(): Int {

        return names.size
    }



    class viewHolder(val view: View) :RecyclerView.ViewHolder(view){

        val nombre : TextView = view.findViewById(R.id.name_selected)
        val mark : TextView = view.findViewById(R.id.mark)


    }


}