(function(global){
  'use strict';
  /**
   * ProjectTableFilter
   * Reusable lightweight table row text filter.
   * Markup requirements:
   *  - Input element: data-project-filter="search" and optional data-project-filter-table selector (defaults to #kt_ecommerce_category_table)
   *  - Table rows to filter: add data-project-row="true" and optional data-search attribute (precomputed searchable text). Fallback is row textContent.
   *  - Static "no projects" row (original data empty): id or selector specified via data-project-filter-static (defaults to #noProjectsStatic)
   *  - "No match" row: id or selector specified via data-project-filter-nofiltered (defaults to #noProjectsFiltered)
   */
  class ProjectTableFilter {
    static init(root){
      root = root || document;
      const inputs = Array.from(root.querySelectorAll('[data-project-filter="search"]'));
      inputs.forEach(input => {
        if(input.dataset.projectFilterInitialized) return; // guard
        input.dataset.projectFilterInitialized = 'true';
        const tableSelector = input.dataset.projectFilterTable || '#kt_ecommerce_category_table';
        const table = root.querySelector(tableSelector);
        if(!table) return;
        const staticNoSel = input.dataset.projectFilterStatic || '#noProjectsStatic';
        const noFilteredSel = input.dataset.projectFilterNofiltered || '#noProjectsFiltered';
        const staticNo = root.querySelector(staticNoSel);
        const noFiltered = root.querySelector(noFilteredSel);
        function rows(){ return Array.from(table.querySelectorAll('tbody tr[data-project-row="true"]')); }
        function apply(){
          const q = input.value.trim().toLowerCase();
          let visible = 0;
          const rws = rows();
          rws.forEach(r => {
            const hay = (r.getAttribute('data-search') || r.textContent).toLowerCase();
            const match = !q || hay.indexOf(q) !== -1;
            r.classList.toggle('d-none', !match);
            if(match) visible++;
          });
          if(rws.length === 0){
            if(staticNo) staticNo.classList.remove('d-none');
            if(noFiltered) noFiltered.classList.add('d-none');
            return;
          }
          if(visible === 0){
            if(noFiltered) noFiltered.classList.remove('d-none');
          } else {
            if(noFiltered) noFiltered.classList.add('d-none');
          }
        }
        input.addEventListener('input', apply);
      });
    }
  }
  // Auto-init on DOMContentLoaded
  function ready(fn){ if(document.readyState !== 'loading'){ fn(); } else { document.addEventListener('DOMContentLoaded', fn); } }
  ready(() => ProjectTableFilter.init());
  global.ProjectTableFilter = ProjectTableFilter;
})(window);
