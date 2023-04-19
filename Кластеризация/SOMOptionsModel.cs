using ClusteringLib;

namespace Кластеризация {

    public class SOMOptionsModel {

        SOMOptionsForm form;

        public ClusteringOptions Options;

        public SOMOptionsModel(SOMOptionsForm form, MainModel mainModel) {
            this.form = form;
            Options = mainModel.GetOptions();
        }

    }

}