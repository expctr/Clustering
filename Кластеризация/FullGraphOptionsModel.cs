using ClusteringLib;

namespace Кластеризация {

    public class FullGraphOptionsModel {

        FullGraphOptionsForm form;

        public ClusteringOptions Options;

        public FullGraphOptionsModel(FullGraphOptionsForm form, MainModel mainModel) {
            this.form = form;
            Options = mainModel.GetOptions();
        }


    }

}