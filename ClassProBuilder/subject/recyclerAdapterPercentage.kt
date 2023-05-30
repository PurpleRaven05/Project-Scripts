package com.jorgebg.classprobuilder.subject

import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.TextView
import androidx.recyclerview.widget.RecyclerView
import com.jorgebg.classprobuilder.R
import com.jorgebg.classprobuilder.databases.subject_ponderation_provisional

class recyclerAdapterPercentage(val percentages : List<subject_ponderation_provisional>, val view: subjectPercentage) : RecyclerView.Adapter<recyclerAdapterPercentage.viewHolder>() {



    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): viewHolder {

        val layoutInflater = LayoutInflater.from(parent.context)
        return viewHolder(layoutInflater.inflate(R.layout.recycler_student_marks, parent, false))
    }

    override fun onBindViewHolder(holder: viewHolder, position: Int) {

        with(percentages[position]){
            holder.nombre.text = percentages[position].name
            holder.porcentaje.text = (percentages[position].weight.toString() +" %")

            holder.itemView.setOnClickListener{
                view.showDialog(percentages[position].name, percentages[position].id)
            }

        }
    }

    override fun getItemCount(): Int {

        return percentages.size
    }

    class viewHolder(val view: View) : RecyclerView.ViewHolder(view){

        val nombre : TextView = view.findViewById(R.id.name_selected)
        val porcentaje : TextView = view.findViewById(R.id.mark)

    }
}